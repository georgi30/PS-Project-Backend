using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class IngredientsRepository : BaseRepository, IIngredientsRepository
    {
        public IngredientsRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Ingredient>> ListAsync()
        {
            return await _context.Ingredients
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {
            var result = await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<Ingredient> FindByIdAsync(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task<List<Ingredient>> FindByRecipeAsync(int recipeId)
        {
            return await _context.Ingredients.Where(ingredient => ingredient.RecipeId == recipeId).ToListAsync();
        }
        
        public async Task<Ingredient> FindByNameAsync(string name)
        {
            return await _context.Ingredients.Where(ingredient => ingredient.Name == name).FirstAsync();
        }

        public void Update(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            _context.SaveChanges();
        }

        public void Remove(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();
        }
    }
}