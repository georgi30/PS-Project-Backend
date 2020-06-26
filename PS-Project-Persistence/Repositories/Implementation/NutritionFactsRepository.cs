using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class NutritionFactsRepository : BaseRepository, INutritionFactsRepository
    {
        public NutritionFactsRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<NutritionFacts>> ListAsync()
        {
            return await _context.NutritionFacts
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<NutritionFacts> AddAsync(NutritionFacts facts)
        {
            var result = await _context.NutritionFacts.AddAsync(facts);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<NutritionFacts> FindByIdAsync(int id)
        {
            return await _context.NutritionFacts.FindAsync(id);
        }
        
        public async Task<NutritionFacts> FindByRecipeAsync(int recipeId)
        {
            return await _context.NutritionFacts.Where(facts => facts.RecipeId == recipeId).FirstAsync();
        }
        
        public void Update(NutritionFacts facts)
        {
            _context.NutritionFacts.Update(facts);
            _context.SaveChanges();
        }

        public void Remove(NutritionFacts facts)
        {
            _context.NutritionFacts.Remove(facts);
            _context.SaveChanges();
        }
    }
}