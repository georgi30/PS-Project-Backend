using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        Task<Category> FindByNameAsync(string name);
        void Update(Category category);
        void Remove(Category category);
    }
}