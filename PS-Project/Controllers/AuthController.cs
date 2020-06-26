using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Auth;
using PS_Project_Model.Services.Interfaces;

namespace PS_Project.Controllers
{
    [Route("/api/auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Login a user.
        /// </summary>
        /// <returns>Authenticated user.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticatedUserResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> LoginAsync(LoginResource resource)
        {
            var result = await _authService.Authenticate(resource.Email, resource.Password);

            if (result == null)
            {
                return BadRequest(new ErrorResource("Invalid Login"));
            }

            return Ok(result);
        }
        
        /// <summary>
        /// Register a user.
        /// </summary>
        /// <returns>Registered user.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisteredUserResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> RegisterAsync(RegisterResource resource)
        {
            var result = await _authService.Register(resource);

            if (result == null)
            {
                return BadRequest(new ErrorResource("Invalid Register. Some of the parameters don't pass validation."));
            }

            return Ok(result);
        }
    }
}