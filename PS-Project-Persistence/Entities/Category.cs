using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        
        public short Active { get; set; } = 1;

        public short Deleted { get; set; } = 0;
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}