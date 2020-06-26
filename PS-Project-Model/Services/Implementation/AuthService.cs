using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Auth;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils;
using PS_Project_Model.Validations;

namespace PS_Project_Model.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly HashingUtils _hashingUtils = new HashingUtils();
        private readonly IApplicationUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public AuthService(IApplicationUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        
        public async Task<AuthenticatedUserResource> Authenticate(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            // return null if user not found
            if (user == null)
                return null;
            
            var validPassword = _hashingUtils.ValidatePassword(password, user.Password);
            
            if (validPassword && AuthenticationValidation.IsLoginValid(email))
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] 
                    {
                        new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticatedUserResource
                {
                    ExpiresAt = DateTime.UtcNow.AddDays(7).ToString(CultureInfo.InvariantCulture),
                    Token = tokenHandler.WriteToken(token)
                };
            }

            return null;
        }

        public async Task<RegisteredUserResource> Register(RegisterResource resource)
        {
            if (!AuthenticationValidation.IsRegisterValid(resource))
            {
                return null;
            }
            
            var user = await _userRepository.AddAsync(
                new ApplicationUser
                {
                    Active = 1,
                    Deleted = 0,
                    CreatedDate = DateTime.Now,
                    Email = resource.Email,
                    Name = resource.Name,
                    Password = _hashingUtils.HashPassword(resource.Password)
                });

            return new RegisteredUserResource
            {
                UserId = user.UserId,
                Active = user.Active,
                CreatedDate = user.CreatedDate,
                Deleted = user.Deleted,
                Email = user.Email,
                Name = user.Name
            };
        }
        
        public async Task<ApplicationUser> GetUserById(int userId)
        {
            return await _userRepository.FindByIdAsync(userId);
        }
    }
}