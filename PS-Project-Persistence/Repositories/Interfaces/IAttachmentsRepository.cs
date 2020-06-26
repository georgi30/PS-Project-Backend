using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IAttachmentsRepository
    {
        Task<IEnumerable<Attachment>> ListAsync();
        Task AddAsync(Attachment attachment);
        Task<Attachment> FindByIdAsync(int id);
        Task<Attachment> FindByFileSlugAsync(string fileSlug);
        void Update(Attachment attachment);
        void Remove(Attachment attachment);
    }
}