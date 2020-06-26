using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> ListAsync();
        Task<CommentsResponse> GetAsync(int id);
        Task<CommentsResponse> SaveAsync(Comment comment);
        Task<IEnumerable<Comment>> FindByRecipe(int recipeId);
        Task<CommentsResponse> UpdateAsync(int id, Comment comment);
        Task<CommentsResponse> DeleteAsync(int id);
    }
}