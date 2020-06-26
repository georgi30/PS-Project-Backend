using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PS_Project_Model.Resources.Attachments
{
    public class SaveAttachmentsResource
    {
        [Required]
        public IFormFile AttachmentFile { get; set; }
    }
}