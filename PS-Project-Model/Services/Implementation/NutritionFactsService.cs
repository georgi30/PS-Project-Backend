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
    public class NutritionFactsService : INutritionFactsService
    {
        private readonly INutritionFactsRepository _nutritionFactsRepository;
        private readonly IMemoryCache _cache;

        public NutritionFactsService(INutritionFactsRepository nutritionFactsRepository , IMemoryCache cache)
        {
            _nutritionFactsRepository = nutritionFactsRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<NutritionFacts>> ListAsync()
        {
            return await _nutritionFactsRepository.ListAsync();
        }

        public async Task<NutritionFacts> FindByRecipeAsync(int recipeId)
        {
            return await _nutritionFactsRepository.FindByRecipeAsync(recipeId);
        }

        public async Task<NutritionFactsResponse> SaveAsync(NutritionFacts facts)
        {
            try
            {
                await _nutritionFactsRepository.AddAsync(facts);
                return new NutritionFactsResponse(facts);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new NutritionFactsResponse($"An error occurred when saving the ingredient: {ex.Message}");
            }
        }
        
        public async Task<NutritionFactsResponse> GetAsync(int id)
        {
            var existingNutritionFact = await _nutritionFactsRepository.FindByIdAsync(id);
            
            if (existingNutritionFact == null)
                return new NutritionFactsResponse("Nutrition Fact not found.");

            return new NutritionFactsResponse(existingNutritionFact);
        }
        
        public async Task<NutritionFactsResponse> DeleteAsync(int id)
        {
            var existingNutritionFact = await _nutritionFactsRepository.FindByIdAsync(id);

            if (existingNutritionFact == null)
                return new NutritionFactsResponse("Nutrition Fact not found.");

            try
            {
                _nutritionFactsRepository.Remove(existingNutritionFact);
                
                return new NutritionFactsResponse(existingNutritionFact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new NutritionFactsResponse($"An error occurred when deleting the nutrition fact: {ex.Message}");
            }
        }
    }
}