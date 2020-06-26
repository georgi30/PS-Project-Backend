using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Resources.Ratings;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface IRatingsUtils
    {
        Task<Rating> ReadyForCreation(SaveRatingResource resource, int recipeId);
        Task<RatingResource> DeleteByUpdate(Rating rating);
    }
}