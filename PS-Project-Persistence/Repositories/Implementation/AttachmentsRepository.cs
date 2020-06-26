using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories.Implementation
{
    public class AttachmentsRepository : BaseRepository, IAttachmentsRepository
    {
        public AttachmentsRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Attachment>> ListAsync()
        {
            return await _context.Attachments
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Attachment attachment)
        {
            await _context.Attachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment> FindByIdAsync(int id)
        {
            return await _context.Attachments.FindAsync(id);
        }
        
        public async Task<Attachment> FindByFileSlugAsync(string slug)
        {
            return await _context.Attachments.SingleOrDefaultAsync(attachment => attachment.FileSlug == slug);;
        }

        public void Update(Attachment attachment)
        {
            _context.Attachments.Update(attachment);
            _context.SaveChanges();
        }

        public void Remove(Attachment attachment)
        {
            _context.Attachments.Remove(attachment);
            _context.SaveChanges();
        }
    }
}