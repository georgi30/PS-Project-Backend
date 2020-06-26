using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class ApplicationUser
    {
        private DateTime _lastUpdated = DateTime.Now;
        
        [Key]
        public int UserId { get; set; }
        
        public short Active { get; set; } = 1;

        public short Deleted { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public DateTime CreatedDate { 
            get => _lastUpdated;
            set => _lastUpdated = value;
        }
    }
}