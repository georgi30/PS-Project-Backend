using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface IRecipesService
    {
        Task<IEnumerable<Recipe>> ListAsync();
        Task<RecipesResponse> GetAsync(int id);
        Task<List<Recipe>> GetByCategoryAsync(string categoryName);
        
        Task<RecipesResponse> SaveAsync(Recipe recipe);
        Task<RecipesResponse> UpdateAsync(int id, Recipe recipe);
        Task<RecipesResponse> DeleteAsync(int id);
    }
}