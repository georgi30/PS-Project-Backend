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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;

        public CategoriesService(ICategoryRepository categoryRepository, IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<CategoriesResponse> GetByNameAsync(string name)
        {
            var existingCategory = await _categoryRepository.FindByNameAsync(name);
            
            if (existingCategory == null)
                return new CategoriesResponse("Category not found.");

            return new CategoriesResponse(existingCategory);
        }

        public async Task<CategoriesResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                return new CategoriesResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoriesResponse($"An error occurred when saving the attachment: {ex.Message}");
            }
        }

        public async Task<CategoriesResponse> GetAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
            
            if (existingCategory == null)
                return new CategoriesResponse("Category not found.");

            return new CategoriesResponse(existingCategory);
        }

        public async Task<CategoriesResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoriesResponse("Category not found.");

            existingCategory.Name = category.Name;
            

            try
            {
                _categoryRepository.Update(existingCategory);
                
                return new CategoriesResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoriesResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<CategoriesResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoriesResponse("Category not found.");

            try
            {
                _categoryRepository.Remove(existingCategory);
                
                return new CategoriesResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoriesResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}