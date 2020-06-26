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
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMemoryCache _cache;

        public CommentsService(ICommentsRepository commentsRepository, IMemoryCache cache)
        {
            _commentsRepository = commentsRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _commentsRepository.ListAsync();
        }

        public async Task<CommentsResponse> SaveAsync(Comment comment)
        {
            try
            {
                await _commentsRepository.AddAsync(comment);
                return new CommentsResponse(comment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CommentsResponse($"An error occurred when saving the comment: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Comment>> FindByRecipe(int recipeId)
        {
            return await _commentsRepository.FindByRecipeAsync(recipeId);
        }
        
        public async Task<CommentsResponse> GetAsync(int id)
        {
            var existingComment = await _commentsRepository.FindByIdAsync(id);
            
            if (existingComment == null)
                return new CommentsResponse("Comment not found.");

            return new CommentsResponse(existingComment);
        }

        public async Task<CommentsResponse> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _commentsRepository.FindByIdAsync(id);

            if (existingComment == null)
                return new CommentsResponse("Category not found.");

            existingComment.Value = comment.Value;
            

            try
            {
                _commentsRepository.Update(existingComment);
                
                return new CommentsResponse(existingComment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CommentsResponse($"An error occurred when updating the comment: {ex.Message}");
            }
        }

        public async Task<CommentsResponse> DeleteAsync(int id)
        {
            var existingComment = await _commentsRepository.FindByIdAsync(id);

            if (existingComment == null)
                return new CommentsResponse("Comment not found.");

            try
            {
                _commentsRepository.Remove(existingComment);
                
                return new CommentsResponse(existingComment);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CommentsResponse($"An error occurred when deleting the comment: {ex.Message}");
            }
        }
    }
}