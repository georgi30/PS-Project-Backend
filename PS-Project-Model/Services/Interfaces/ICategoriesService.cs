using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<CategoriesResponse> GetAsync(int id);
        Task<CategoriesResponse> GetByNameAsync(string name);
        Task<CategoriesResponse> SaveAsync(Category category);
        Task<CategoriesResponse> UpdateAsync(int id, Category category);
        Task<CategoriesResponse> DeleteAsync(int id);
    }
}