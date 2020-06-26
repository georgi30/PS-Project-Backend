using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> ListAsync();
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> FindByIdAsync(int id);
        Task<IEnumerable<Comment>> FindByRecipeAsync(int recipeId);
        void Update(Comment user);
        void Remove(Comment user);
    }
}