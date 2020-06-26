using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        
        public string ShortSummary { get; set; }
        public string Tags { get; set; }
        
        public string Directions { get; set; }
        
        public string Yield { get; set; }
        
        public string PreparationTime { get; set; }
        
        public string CookingTime { get; set; }

        public string Category { get; set; }
        
        public int WhoUserId { get; set; }
        
        public short Active { get; set; } = 1;

        public short Deleted { get; set; }
        
        public string RecipeSlug { get; set; }
        
        public int AttachmentId { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}