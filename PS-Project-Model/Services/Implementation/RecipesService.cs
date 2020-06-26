using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using Persistence.Repositories.Interfaces;
using PS_Project_Model.Responses;
using PS_Project_Model.Services.Interfaces;

namespace PS_Project_Model.Services.Implementation
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository _recipesRepository;

        public RecipesService(IRecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
        }

        public async Task<IEnumerable<Recipe>> ListAsync()
        {

            return await _recipesRepository.ListAsync();
        }

        public async Task<List<Recipe>> GetByCategoryAsync(string categoryName)
        {
            return await _recipesRepository.FindByCategoryAsync(categoryName);
        }

        public async Task<RecipesResponse> SaveAsync(Recipe recipe)
        {
            try
            {
                await _recipesRepository.AddAsync(recipe);
                return new RecipesResponse(recipe);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RecipesResponse($"An error occurred when saving the recipe: {ex.Message}");
            }
        }

        public async Task<RecipesResponse> GetAsync(int id)
        {
            var existingRecipe = await _recipesRepository.FindByIdAsync(id);
            
            if (existingRecipe == null)
                return new RecipesResponse("Recipe not found.");

            return new RecipesResponse(existingRecipe);
        }
        

        public async Task<RecipesResponse> UpdateAsync(int id, Recipe recipe)
        {
            var existingRecipe = await _recipesRepository.FindByIdAsync(id);

            if (existingRecipe == null)
                return new RecipesResponse("Recipe not found.");

            /*existingRecipe.Username = user.Username;
            existingRecipe.Password = user.Password;
            existingRecipe.Email = user.Email;
            existingRecipe.Name = user.Name;
            existingRecipe.Phone = user.Phone;
            existingRecipe.Active = user.Active;
            existingRecipe.Deleted = user.Deleted;
            existingRecipe.LastUpdated = DateTime.Now;
            existingRecipe.WhoUserId = user.WhoUserId;*/

            try
            {
                _recipesRepository.Update(existingRecipe);
                
                return new RecipesResponse(existingRecipe);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RecipesResponse($"An error occurred when updating the recipe: {ex.Message}");
            }
        }

        public async Task<RecipesResponse> DeleteAsync(int id)
        {
            var existingRecipe = await _recipesRepository.FindByIdAsync(id);

            if (existingRecipe == null)
                return new RecipesResponse("Application user not found.");

            try
            {
                _recipesRepository.Remove(existingRecipe);
                
                return new RecipesResponse(existingRecipe);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RecipesResponse($"An error occurred when deleting the recipe: {ex.Message}");
            }
        }
    }
}