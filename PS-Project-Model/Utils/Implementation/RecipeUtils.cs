using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iTextSharp.text;
using Persistence.Entities;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Helpers;
using PS_Project_Model.Resources.Recipes;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;
using static System.DateTime;

namespace PS_Project_Model.Utils.Implementation
{
    public class RecipeUtils : IRecipeUtils
    {
        private IRatingsService _ratingsService;
        private IRecipesService _recipesService;
        private IRequestUtils _requestUtils;
        private IIngredientsService _ingredientsService;
        private IMapper _mapper;
        private INutritionFactsService _nutritionFactsService;
        private IAuthService _authService;
        private ICategoriesService _categoriesService;
        private IAttachmentsService _attachmentsService;

        public RecipeUtils(IRecipesService recipesService, IRequestUtils requestUtils, 
                           IRatingsService ratingsService, IIngredientsService ingredientsService,
                           IMapper mapper, INutritionFactsService nutritionFactsService,
                           IAuthService authService, ICategoriesService categoriesService,
                           IAttachmentsService attachmentsService)
        {
            _recipesService = recipesService;
            _requestUtils = requestUtils;
            _ratingsService = ratingsService;
            _ingredientsService = ingredientsService;
            _nutritionFactsService = nutritionFactsService;
            _mapper = mapper;
            _authService = authService;
            _categoriesService = categoriesService;
            _attachmentsService = attachmentsService;
        }

        public async Task<RecipeResource> PrepareForCreation(SaveRecipeResource resource, string token)
        {
            var category =  await _categoriesService.GetAsync(resource.CategoryId);
            var recipe = _mapper.Map<SaveRecipeResource, Recipe>(resource);
            int userId = token != null ? _requestUtils.GetUserIdFromToken(token) : 0;
            recipe.WhoUserId = userId;
            recipe.Category = category.Resource.Name;
            
            var recipeResult = await _recipesService.SaveAsync(recipe);
            
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var ingredientResource in resource.Ingredients)
            {
                var ingredient = new Ingredient
                {
                    Name = ingredientResource.Name,
                    Notes = ingredientResource.Notes,
                    RecipeId = recipeResult.Resource.RecipeId
                };

                var result = await _ingredientsService.SaveAsync(ingredient);
                
                ingredients.Add(result.Resource);
            }

            var nutritionFactsResult = await _nutritionFactsService.SaveAsync(new NutritionFacts
            {
                Calories = resource.NutritionFacts.Calories,
                Cholesterol = resource.NutritionFacts.Cholesterol,
                Protein = resource.NutritionFacts.Protein,
                RecipeId = recipeResult.Resource.RecipeId,
                TotalCarbohydrate = resource.NutritionFacts.TotalCarbohydrate,
                TotalFat = resource.NutritionFacts.TotalFat
            });

            var recipeResource = _mapper.Map<Recipe, RecipeResource>(recipeResult.Resource);
            recipeResource.Ingredients = ingredients;
            recipeResource.NutritionFacts = nutritionFactsResult.Resource;
            recipeResource.Category = category.Resource.Name;
            
            return recipeResource;
        }

        public async Task<List<RecipeResource>> SearchForRecipe(SearchResource resource)
        {
            if (resource.CategoryName == String.Empty && resource.Ingredients.Count == 0 && resource.SearchTerm == String.Empty)
            {
                return new List<RecipeResource>();
            }
            
            var result = new List<RecipeResource>();

            var allRecipes = await _recipesService.ListAsync();
            
            var foundRecipes = allRecipes.Where(recipe => recipe.Category == resource.CategoryName).ToList();

            if (resource.SearchTerm != String.Empty)
            {

                foundRecipes = foundRecipes.Where(recipe => recipe.Directions.Contains(resource.SearchTerm)
                                                            || recipe.Name.Contains(resource.SearchTerm)).ToList();
            }

            if (resource.Ingredients.Count > 0)
            {
                result = await IngredientsIncluded(foundRecipes, resource.Ingredients, resource.IncludeAllIngredients);
            }
            else
            {
                foreach (var recipe in foundRecipes)
                {
                    result.Add(await PrepareForListing(recipe));
                }
            }

            return result;
        }

        private async Task<List<RecipeResource>> IngredientsIncluded(List<Recipe> recipes, List<string> ingredientNames, bool allIngredients)
        {
            bool result = false;
            var resources = new List<RecipeResource>();
            
            var ingredientsSearch = await GetIngredientsByName(ingredientNames);
            foreach (var recipe in recipes)
            {
                
                var ingredients = await _ingredientsService.FindByRecipeAsync(recipe.RecipeId);
                if (allIngredients)
                {
                    result = !ingredients.Except(ingredientsSearch).Any();
                }
                else
                {
                    result = ingredients.Intersect(ingredientsSearch).Any();
                }

                if (result)
                {
                    resources.Add(await PrepareForListing(recipe));
                }
            }

            return resources;
        }

        private async Task<List<Ingredient>> GetIngredientsByName(List<string> ingredientNames)
        {
            var result = new List<Ingredient>();

            foreach (var ingredientName in ingredientNames)
            {
                var ingredient = await _ingredientsService.FindByNameAsync(ingredientName);
                result.Add(ingredient);
            }

            return result;
        }

        public async Task<TopRecipesResource> GetTopRecipes()
        {
            var breakfastRecipes = await GetRecipesSortedByRating(await _recipesService.GetByCategoryAsync("Закуска"));
            var lunchRecipes = await GetRecipesSortedByRating(await _recipesService.GetByCategoryAsync("Обяд"));
            var dinnerRecipes = await GetRecipesSortedByRating(await _recipesService.GetByCategoryAsync("Вечеря"));

            Recipe breakfastRecipe = breakfastRecipes.Count > 0 ? (await _recipesService.GetAsync(breakfastRecipes[0].RecipeId)).Resource : null;
            Recipe lunchRecipe = lunchRecipes.Count > 0 ? (await _recipesService.GetAsync(lunchRecipes[0].RecipeId)).Resource : null;
            Recipe dinnerRecipe = dinnerRecipes.Count > 0 ? (await _recipesService.GetAsync(dinnerRecipes[0].RecipeId)).Resource : null;
            
            return new TopRecipesResource
            {
                Breakfast = await PrepareForListing(breakfastRecipe),
                Lunch = await PrepareForListing(lunchRecipe),
                Dinner = await PrepareForListing(dinnerRecipe)
            };
        }

        public async Task<RecipeResource> PrepareForListing(Recipe recipe)
        {
            if (recipe == null)
            {
                return null;
            }
            
            var ingredients = await _ingredientsService.FindByRecipeAsync(recipe.RecipeId);
            var nutritionFactsResult = await _nutritionFactsService.FindByRecipeAsync(recipe.RecipeId);
            
            var resource = _mapper.Map<Recipe, RecipeResource>(recipe);
            resource.Ingredients = ingredients;
            resource.NutritionFacts = nutritionFactsResult;
            resource.User = await _authService.GetUserById(recipe.WhoUserId);
            resource.Rating = await GetAverageRatingForRecipe(recipe.RecipeId);
            resource.AttachmentUrl = await BuildAttachmentUrl(recipe.AttachmentId);

            return resource;
        }

        public async Task<Recipe> PrepareForDeletion(int id, string token)
        {
            int userId = token != null ? _requestUtils.GetUserIdFromToken(token) : 0;
            var result = await _recipesService.GetAsync(id);

            if (!result.Success)
            {
                return null;
            }

            var recipe = result.Resource;
            
            recipe.Active = 0;
            recipe.Deleted = 1;
            recipe.LastUpdated = Now;
            recipe.WhoUserId = userId;

            return recipe;
        }

        private async Task<List<RatingHelper>> GetRecipesSortedByRating(List<Recipe> recipes)
        {
            var result = new List<RatingHelper>();
            foreach (var recipe in recipes)
            {
                var averageRating = await GetAverageRatingForRecipe(recipe.RecipeId);

                result.Add(new RatingHelper
                {
                    RecipeId = recipe.RecipeId,
                    AverageRating = averageRating
                });
            }

            return result.OrderByDescending(recipe => recipe.AverageRating).ToList();
        }

        private async Task<double> GetAverageRatingForRecipe(int recipeId)
        {
            var ratings = await _ratingsService.GetAllByRecipeAsync(recipeId);
            var averageRating = 0;
                
            if (ratings.Count > 0)
            {
                averageRating = ratings.Sum(rating => rating.Score) / ratings.Count;
            }

            return averageRating;
        }

        private async Task<string> BuildAttachmentUrl(int attachmentId)
        {
            var baseURL = "http://192.168.16.86:4001";

            var attachment = await _attachmentsService.GetAsync(attachmentId);
            
            return $"{baseURL}/{attachment.Resource.FileSlug}";
        }
    }
}