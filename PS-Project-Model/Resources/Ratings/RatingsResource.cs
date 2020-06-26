using Persistence.Entities;

namespace PS_Project_Model.Resources.Ratings
{
    public class RatingResource
    {
        public int Rating { get; set; }
        public Recipe Recipe { get; set; }
    }
}