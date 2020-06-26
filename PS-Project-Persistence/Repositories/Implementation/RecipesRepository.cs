using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class RecipesRepository : BaseRepository, IRecipesRepository
    {
        public RecipesRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Recipe>> ListAsync()
        {
            return await _context.Recipes
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<Recipe> FindByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);;
        }

        public async Task<List<Recipe>> FindByCategoryAsync(string categoryName)
        {
            return await _context.Recipes.Where(recipe => recipe.Category == categoryName).ToListAsync();
        }

        public void Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
        }

        public void Remove(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }
    }
}