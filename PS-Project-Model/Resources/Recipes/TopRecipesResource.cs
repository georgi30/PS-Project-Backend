using Persistence.Entities;

namespace PS_Project_Model.Resources.Recipes
{
    public class TopRecipesResource
    {
        public RecipeResource Breakfast { get; set; }
        public RecipeResource Lunch { get; set; }
        public RecipeResource Dinner { get; set; }
    }
}