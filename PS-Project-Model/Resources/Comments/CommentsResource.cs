using System;
using Persistence.Entities;

namespace PS_Project_Model.Resources.Comments
{
    public class CommentsResource
    {
        public int CommentId { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
        public Recipe Recipe { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}