using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources.Auth;

namespace PS_Project_Model.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticatedUserResource> Authenticate(string email, string password);
        Task<RegisteredUserResource> Register(RegisterResource resource);
        Task<ApplicationUser> GetUserById(int userId);
    }
}