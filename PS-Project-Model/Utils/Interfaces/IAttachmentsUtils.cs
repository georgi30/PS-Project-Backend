using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources.Attachments;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface IAttachmentUtils
    {
        Attachment ReadyForCreation(SaveAttachmentsResource resource);
        Task<AttachmentsResource> DeleteByUpdate(Attachment attachment);
        string GetFileType(string filename);
    }
}