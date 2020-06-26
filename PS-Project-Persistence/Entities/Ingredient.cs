using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        
        public int RecipeId { get; set; }
    }
}