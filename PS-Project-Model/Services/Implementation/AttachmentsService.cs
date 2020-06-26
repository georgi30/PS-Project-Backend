using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;
using PS_Project_Model.Responses;
using PS_Project_Model.Services.Interfaces;

namespace PS_Project_Model.Services.Implementation
{
    public class AttachmentsService : IAttachmentsService
    {
        private readonly IAttachmentsRepository _attachmentsRepository;
        private readonly IMemoryCache _cache;

        public AttachmentsService(IAttachmentsRepository attachmentsRepository, IMemoryCache cache)
        {
            _attachmentsRepository = attachmentsRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Attachment>> ListAsync()
        {
            return await _attachmentsRepository.ListAsync();
        }

        public async Task<AttachmentsResponse> SaveAsync(Attachment attachment)
        {
            try
            {
                await _attachmentsRepository.AddAsync(attachment);
                return new AttachmentsResponse(attachment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AttachmentsResponse($"An error occurred when saving the attachment: {ex.Message}");
            }
        }

        public async Task<AttachmentsResponse> GetAsync(int id)
        {
            var existingAttachment = await _attachmentsRepository.FindByIdAsync(id);
            
            if (existingAttachment == null)
                return new AttachmentsResponse("Attachment not found.");

            return new AttachmentsResponse(existingAttachment);
        }
        
        public async Task<AttachmentsResponse> GetByFileSlugAsync(string fileSlug)
        {
            var existingAttachment = await _attachmentsRepository.FindByFileSlugAsync(fileSlug);
            
            if (existingAttachment == null)
                return new AttachmentsResponse("Attachment not found.");

            return new AttachmentsResponse(existingAttachment);
        }

        public async Task<AttachmentsResponse> UpdateAsync(int id, Attachment attachment)
        {
            var existingAttachment = await _attachmentsRepository.FindByIdAsync(id);

            if (existingAttachment == null)
                return new AttachmentsResponse("Attachment not found.");

            existingAttachment.FilePath = attachment.FilePath;
            existingAttachment.FileSlug = attachment.FileSlug;
            existingAttachment.LastUpdated = DateTime.Now;
            

            try
            {
                _attachmentsRepository.Update(existingAttachment);
                
                return new AttachmentsResponse(existingAttachment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AttachmentsResponse($"An error occurred when updating the attachment: {ex.Message}");
            }
        }

        public async Task<AttachmentsResponse> DeleteAsync(int id)
        {
            var existingAttachment = await _attachmentsRepository.FindByIdAsync(id);

            if (existingAttachment == null)
                return new AttachmentsResponse("Attachment not found.");

            try
            {
                _attachmentsRepository.Remove(existingAttachment);
                
                return new AttachmentsResponse(existingAttachment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AttachmentsResponse($"An error occurred when deleting the attachment: {ex.Message}");
            }
        }
    }
}