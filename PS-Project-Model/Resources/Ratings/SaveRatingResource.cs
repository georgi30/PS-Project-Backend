using System.ComponentModel.DataAnnotations;

namespace PS_Project_Model.Resources.Ratings
{
    public class SaveRatingResource
    {
        [Required]
        public int Rating { get; set; }
    }
}