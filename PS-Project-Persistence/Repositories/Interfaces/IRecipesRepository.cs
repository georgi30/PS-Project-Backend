using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IRecipesRepository
    {
        Task<IEnumerable<Recipe>> ListAsync();
        Task AddAsync(Recipe recipe);
        Task<Recipe> FindByIdAsync(int id);
        Task<List<Recipe>> FindByCategoryAsync(string categoryName);
        void Update(Recipe user);
        void Remove(Recipe user);
    }
}