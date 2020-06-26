using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Recipes;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface IRecipeUtils
    {
        Task<Recipe> PrepareForDeletion(int id, string token);
        Task<RecipeResource> PrepareForCreation(SaveRecipeResource recipe, string token);
        Task<List<RecipeResource>> SearchForRecipe(SearchResource resource);
        Task<TopRecipesResource> GetTopRecipes();
        Task<RecipeResource> PrepareForListing(Recipe resultResource);
    }
}