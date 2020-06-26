using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Persistence.Entities;
using PS_Project_Model.Resources.Attachments;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class CategoriesUtils : ICategoriesUtils
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;
        private static Random random = new Random();

        public CategoriesUtils(ICategoriesService categoriesService, IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        public Category ReadyForCreation(SaveCategoryResources resource)
        {
            var category = _mapper.Map<SaveCategoryResources, Category>(resource);
            category.Active = 1;
            category.Deleted = 0;
            
            return category;
        }

        public async Task<CategoryResource> DeleteByUpdate(Category category)
        {
            category.Active = 0;
            category.Deleted = 1;
            category.LastUpdated = DateTime.Now;
            await _categoriesService.UpdateAsync(category.CategoryId, category);

            return _mapper.Map<Category, CategoryResource>(category);
        }
    }
}