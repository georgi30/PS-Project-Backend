using System;
using System.Collections.Generic;
using Persistence.Entities;
using PS_Project_Model.Resources.Others;

namespace PS_Project_Model.Resources.Recipes
{
    public class RecipeResource
    {
        public int RecipeId { get; set; }
        
        public string Name { get; set; }
        
        public string ShortSummary { get; set; }
        
        public string Tags { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; }
        
        public string Directions { get; set; }
        
        public string Yield { get; set; }
        
        public string PreparationTime { get; set; }
        
        public string CookingTime { get; set; }
        
        public string Category { get; set; }
        
        public double Rating { get; set; }
        
        public NutritionFacts NutritionFacts { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public short Active { get; set; }
        public short Deleted { get; set; }
        
        public string AttachmentUrl { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}