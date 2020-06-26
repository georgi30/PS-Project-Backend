using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IIngredientsRepository
    {
        Task<IEnumerable<Ingredient>> ListAsync();
        Task<Ingredient> AddAsync(Ingredient ingredient);
        Task<Ingredient> FindByIdAsync(int id);
        Task<List<Ingredient>> FindByRecipeAsync(int recipeId);
        Task<Ingredient> FindByNameAsync(string name);
        void Update(Ingredient ingredient);
        void Remove(Ingredient ingredient);
    }
}