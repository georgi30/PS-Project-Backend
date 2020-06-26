using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class ApplicationUserRepository : BaseRepository, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ApplicationUser>> ListAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<ApplicationUser> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email && user.Active == 1);
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Remove(ApplicationUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}