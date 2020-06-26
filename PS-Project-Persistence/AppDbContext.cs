using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<NutritionFacts> NutritionFacts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}