using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface IRatingsService
    {
        Task<IEnumerable<Rating>> ListAsync();
        Task<RatingsResponse> GetAsync(int id);
        Task<List<Rating>> GetAllByRecipeAsync(int recipeId);
        Task<RatingsResponse> SaveAsync(Rating rating);
        Task<RatingsResponse> UpdateAsync(int id, Rating rating);
        Task<RatingsResponse> DeleteAsync(int id);
    }
}