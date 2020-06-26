using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<IEnumerable<ApplicationUser>> ListAsync();
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task<ApplicationUser> FindByIdAsync(int id);
        Task<ApplicationUser> FindByEmailAsync(string email);
        void Update(ApplicationUser user);
        void Remove(ApplicationUser user);
    }
}