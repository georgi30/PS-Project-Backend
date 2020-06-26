using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Responses;

namespace PS_Project_Model.Services.Interfaces
{
    public interface IAttachmentsService
    {
        Task<IEnumerable<Attachment>> ListAsync();
        Task<AttachmentsResponse> GetAsync(int id);
        Task<AttachmentsResponse> GetByFileSlugAsync(string slug);
        Task<AttachmentsResponse> SaveAsync(Attachment attachment);
        Task<AttachmentsResponse> UpdateAsync(int id, Attachment attachment);
        Task<AttachmentsResponse> DeleteAsync(int id);
    }
}