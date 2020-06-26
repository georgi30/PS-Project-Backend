using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Persistence.Entities;
using PS_Project_Model.Resources.Attachments;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class AttachmentUtils : IAttachmentUtils
    {
        private readonly IAttachmentsService _attachmentsService;
        private readonly IMapper _mapper;
        private static Random random = new Random();

        public AttachmentUtils(IAttachmentsService attachmentsService, IMapper mapper)
        {
            _attachmentsService = attachmentsService;
            _mapper = mapper;
        }
        
        public string GetFileType(string filename)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            
            if(!provider.TryGetContentType(filename, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        public Attachment ReadyForCreation(SaveAttachmentsResource resource)
        {
            var slug = GenerateRandomString(10);
            var attachment = _mapper.Map<SaveAttachmentsResource, Attachment>(resource);
            var fileName = slug + ".jpg";
            attachment.Active = 1;
            attachment.Deleted = 0;
            attachment.FilePath = UploadFile(resource.AttachmentFile, fileName);
            attachment.FileSlug = slug;
            
            return attachment;
        }

        public async Task<AttachmentsResource> DeleteByUpdate(Attachment attachment)
        {
            attachment.Active = 0;
            attachment.Deleted = 1;
            attachment.LastUpdated = DateTime.Now;
            await _attachmentsService.UpdateAsync(attachment.AttachmentId, attachment);

            return _mapper.Map<Attachment, AttachmentsResource>(attachment);
        }

        private string UploadFile(IFormFile file, string fileName)
        {
            var pathToSave = "/opt/ps/";
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(pathToSave, fileName);
 
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return dbPath;
        }
        
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}