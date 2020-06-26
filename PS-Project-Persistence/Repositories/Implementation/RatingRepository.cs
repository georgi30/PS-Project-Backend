using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class RatingRepository : BaseRepository, IRatingRepository
    {
        public RatingRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Rating>> ListAsync()
        {
            return await _context.Ratings
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<Rating> AddAsync(Rating rating)
        {
            var result = await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Rating> FindByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<List<Rating>> FindAllByRecipeAsync(int recipeId)
        {
            return await _context.Ratings.Where(rating => rating.Recipe.RecipeId == recipeId).ToListAsync();
        }

        public void Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            _context.SaveChanges();
        }

        public void Remove(Rating rating)
        {
            _context.Ratings.Remove(rating);
            _context.SaveChanges();
        }
    }
}