using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class CommentsRepository : BaseRepository, ICommentsRepository
    {
        public CommentsRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _context.Comments
                .AsNoTracking()
                .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            var result = await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task<Comment> FindByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> FindByRecipeAsync(int recipeId)
        {
            return await _context.Comments.Where(comment => comment.RecipeId == recipeId).ToListAsync();
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void Remove(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}