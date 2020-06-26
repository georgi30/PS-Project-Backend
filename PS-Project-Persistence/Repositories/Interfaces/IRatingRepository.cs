using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> ListAsync();
        Task<Rating> AddAsync(Rating rating);
        Task<Rating> FindByIdAsync(int id);
        Task<List<Rating>> FindAllByRecipeAsync(int recipeId);
        void Update(Rating rating);
        void Remove(Rating rating);
    }
}