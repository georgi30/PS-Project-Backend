using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PS_Project_Model.Resources;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class RequestUtils : IRequestUtils
    {
        private readonly IOptions<AppSettings> _appSettings;

        public RequestUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }
        
        public int GetUserIdFromToken(string token)
        {
            Console.WriteLine("Token: {0}", token);
            token = token.Replace("Bearer ", string.Empty);
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return Int32.Parse(claims.Identity.Name ?? string.Empty);
        }
    }
}