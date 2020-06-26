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
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMemoryCache _cache;

        public IngredientsService(IIngredientsRepository ingredientsRepository, IMemoryCache cache)
        {
            _ingredientsRepository = ingredientsRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Ingredient>> ListAsync()
        {
            return await _ingredientsRepository.ListAsync();
        }

        public async Task<List<Ingredient>> FindByRecipeAsync(int recipeId)
        {
            return await _ingredientsRepository.FindByRecipeAsync(recipeId);
        }

        public async Task<Ingredient> FindByNameAsync(string name)
        {
            return await _ingredientsRepository.FindByNameAsync(name);
        }
        
        public async Task<IngredientsResponse> SaveAsync(Ingredient ingredient)
        {
            try
            {
                await _ingredientsRepository.AddAsync(ingredient);
                return new IngredientsResponse(ingredient);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new IngredientsResponse($"An error occurred when saving the ingredient: {ex.Message}");
            }
        }
        
        public async Task<IngredientsResponse> GetAsync(int id)
        {
            var existingIngredient = await _ingredientsRepository.FindByIdAsync(id);
            
            if (existingIngredient == null)
                return new IngredientsResponse("Ingredient not found.");

            return new IngredientsResponse(existingIngredient);
        }
        
        public async Task<IngredientsResponse> DeleteAsync(int id)
        {
            var existingIngredient = await _ingredientsRepository.FindByIdAsync(id);

            if (existingIngredient == null)
                return new IngredientsResponse("Ingredient not found.");

            try
            {
                _ingredientsRepository.Remove(existingIngredient);
                
                return new IngredientsResponse(existingIngredient);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new IngredientsResponse($"An error occurred when deleting the ingredient: {ex.Message}");
            }
        }
    }
}