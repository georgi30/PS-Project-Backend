using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Value { get; set; }
        public int WhoUserId { get; set; }
        public int RecipeId{ get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}