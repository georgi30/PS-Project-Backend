using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class NutritionFacts
    {
        [Key]
        public int Id { get; set; }
        public string Calories { get; set; }
        public string TotalCarbohydrate { get; set; }
        public string TotalFat { get; set; }
        public string Protein { get; set; }
        public string Cholesterol { get; set; }
        
        public int RecipeId { get; set; }
    }
}