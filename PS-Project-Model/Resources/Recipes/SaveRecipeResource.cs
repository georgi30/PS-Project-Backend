using System.Collections.Generic;
using Persistence.Entities;
using PS_Project_Model.Resources.Others;

namespace PS_Project_Model.Resources.Recipes
{
    public class SaveRecipeResource
    {
        public string Name { get; set; }
        
        public string ShortSummary { get; set; }
        
        public string Tags { get; set; }
        
        public ICollection<IngredientResource> Ingredients { get; set; }
        
        public string Directions { get; set; }
        
        public string Yield { get; set; }
        
        public string PreparationTime { get; set; }
        
        public string CookingTime { get; set; }
        
        public int CategoryId { get; set; }
        
        public int AttachmentId { get; set; }
        
        public NutritionFactsResource NutritionFacts { get; set; }
    }
}