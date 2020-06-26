using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface INutritionFactsService
    {
        Task<IEnumerable<NutritionFacts>> ListAsync();
        Task<NutritionFactsResponse> GetAsync(int id);
        Task<NutritionFacts> FindByRecipeAsync(int recipeId);
        Task<NutritionFactsResponse> SaveAsync(NutritionFacts facts);
        Task<NutritionFactsResponse> DeleteAsync(int id);
    }
}