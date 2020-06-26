using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public int Score { get; set; }
        public Recipe Recipe { get; set; }
    }
}