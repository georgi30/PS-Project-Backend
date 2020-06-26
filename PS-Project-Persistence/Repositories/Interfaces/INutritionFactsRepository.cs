using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface INutritionFactsRepository
    {
        Task<IEnumerable<NutritionFacts>> ListAsync();
        Task<NutritionFacts> AddAsync(NutritionFacts facts);
        Task<NutritionFacts> FindByIdAsync(int id);
        Task<NutritionFacts> FindByRecipeAsync(int recipeId);
        void Update(NutritionFacts facts);
        void Remove(NutritionFacts facts);
    }
}