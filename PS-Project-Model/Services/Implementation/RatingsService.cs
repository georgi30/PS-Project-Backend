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
    public class RatingsService : IRatingsService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMemoryCache _cache;

        public RatingsService(IRatingRepository ratingRepository, IMemoryCache cache)
        {
            _ratingRepository = ratingRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Rating>> ListAsync()
        {
            return await _ratingRepository.ListAsync();
        }

        public async Task<List<Rating>> GetAllByRecipeAsync(int recipeId)
        {
            return await _ratingRepository.FindAllByRecipeAsync(recipeId);
        }

        public async Task<RatingsResponse> SaveAsync(Rating rating)
        {
            try
            {
                await _ratingRepository.AddAsync(rating);
                return new RatingsResponse(rating);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RatingsResponse($"An error occurred when saving the ratings: {ex.Message}");
            }
        }

        public async Task<RatingsResponse> GetAsync(int id)
        {
            var existingRating = await _ratingRepository.FindByIdAsync(id);
            
            if (existingRating == null)
                return new RatingsResponse("Rating not found.");

            return new RatingsResponse(existingRating);
        }

        public async Task<RatingsResponse> UpdateAsync(int id, Rating rating)
        {
            var existingRating = await _ratingRepository.FindByIdAsync(id);

            if (existingRating == null)
                return new RatingsResponse("Rating not found.");

            existingRating.Score = existingRating.Score;
            

            try
            {
                _ratingRepository.Update(existingRating);
                
                return new RatingsResponse(existingRating);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RatingsResponse($"An error occurred when updating the rating: {ex.Message}");
            }
        }

        public async Task<RatingsResponse> DeleteAsync(int id)
        {
            var existingRating = await _ratingRepository.FindByIdAsync(id);

            if (existingRating == null)
                return new RatingsResponse("Rating not found.");

            try
            {
                _ratingRepository.Remove(existingRating);
                
                return new RatingsResponse(existingRating);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RatingsResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}