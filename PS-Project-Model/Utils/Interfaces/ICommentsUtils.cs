using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Resources.Comments;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface ICommentsUtils
    {
        Task<Comment> ReadyForCreation(SaveCommentResource resource, string token);
        Task<CommentsResource> PrepareForListing(Comment comment);
    }
}