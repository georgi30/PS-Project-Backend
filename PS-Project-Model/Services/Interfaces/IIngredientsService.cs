using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<IEnumerable<Ingredient>> ListAsync();
        Task<IngredientsResponse> GetAsync(int id);
        Task<List<Ingredient>> FindByRecipeAsync(int recipeId);
        Task<Ingredient> FindByNameAsync(string name);
        Task<IngredientsResponse> SaveAsync(Ingredient ingredient);
        Task<IngredientsResponse> DeleteAsync(int id);
    }
}