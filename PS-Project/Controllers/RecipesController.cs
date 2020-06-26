using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Persistence.Entities;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Ratings;
using PS_Project_Model.Resources.Recipes;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project.Controllers
{
    [Authorize]
    [Route("/api/recipes")]
    [Produces("application/json")]
    [ApiController]
    public class RecipesController : Controller
    {
        private readonly IRecipesService _recipesService;
        private readonly IRatingsService _ratingsService;
        private readonly IMapper _mapper;
        private readonly IRecipeUtils _utils;
        private readonly IRatingsUtils _ratingsUtils;
        private readonly IPdfGeneratorUtils _pdfGeneratorUtils;

        public RecipesController(IRecipesService recipesService, IRatingsService ratingsService, IMapper mapper, 
            IRecipeUtils utils, IRatingsUtils ratingsUtils, IPdfGeneratorUtils pdfGeneratorUtils)
        {
            _recipesService = recipesService;
            _ratingsService = ratingsService;
            _ratingsUtils = ratingsUtils;
            _mapper = mapper;
            _utils = utils;
            _pdfGeneratorUtils = pdfGeneratorUtils;
        }

        /// <summary>
        /// Lists all recipes.
        /// </summary>
        /// <returns>List of all recipes.</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RecipeResource>), 200)]
        public async Task<IEnumerable<RecipeResource>> ListAsync()
        {
            List<RecipeResource> resources = new List<RecipeResource>();
            var recipes = await _recipesService.ListAsync();

            foreach (var recipe in recipes)
            {
                resources.Add(await _utils.PrepareForListing(recipe));
            }

            return resources;
        }

        /// <summary>
        /// Gets an existing recipe according to an identifier.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecipeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _recipesService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var recipeResource = await _utils.PrepareForListing(result.Resource);
            return Ok(recipeResource);
        }
        
        /// <summary>
        /// Gets top recipes.
        /// </summary>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpGet("top")]
        [ProducesResponseType(typeof(RecipeResource), 200)]
        public async Task<IActionResult> GetTopRecipesAsync()
        {
            return Ok(await _utils.GetTopRecipes());
        }
        
        /// <summary>
        /// Export recipe.
        /// </summary>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpGet("{id}/export")]
        [ProducesResponseType(typeof(RecipeResource), 200)]
        public async Task<IActionResult> ExportRecipe(int id)
        {
            var result = await _recipesService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            
            var pdfPath = _pdfGeneratorUtils.GeneratePdf(result.Resource);
            
            return new PhysicalFileResult(pdfPath, "application/octet-stream");
        }
        
        /// <summary>
        ///   Saves a new recipe.
        /// </summary>
        /// <param name="resource">Recipe data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RecipeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveRecipeResource resource)
        {
            var recipe = await _utils.PrepareForCreation(resource, Request.Headers[HeaderNames.Authorization]);
            return Ok(recipe);
        }
        
        /// <summary>
        ///   Saves a new rating.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <param name="resource">Rating data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost("{id}/rating")]
        [ProducesResponseType(typeof(RecipeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostRatingAsync(int id, [FromBody] SaveRatingResource resource)
        {
            var rating = await _ratingsUtils.ReadyForCreation(resource, id);
            
            if (rating == null)
            {
                return BadRequest(new ErrorResource("Could not find recipe with id:" + id));
            }
            
            var result = await _ratingsService.SaveAsync(rating);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            
            return Ok(result.Resource);
        }

        /// <summary>
        /// Updates an existing recipe according to an identifier.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <param name="resource">Updated recipe data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RecipeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateRecipeResource resource)
        {
            var recipe = _mapper.Map<UpdateRecipeResource, Recipe>(resource);

            var result = await _recipesService.UpdateAsync(id, recipe);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var userResource = _mapper.Map<Recipe, RecipeResource>(result.Resource);
            return Ok(userResource);
        }
        
        /// <summary>
        /// Search for recipes according to search parameters.
        /// </summary>
        /// <returns>Response for the request.</returns>
        [AllowAnonymous]
        [HttpPost("search")]
        [ProducesResponseType(typeof(TopRecipesResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SearchAsync([FromBody] SearchResource resource)
        {
            var foundRecipes = await _utils.SearchForRecipe(resource);
            return Ok(foundRecipes);
        }

        /// <summary>
        /// Deletes a given recipe according to an identifier.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RecipeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var recipe = _utils.PrepareForDeletion(id, Request.Headers[HeaderNames.Authorization]);

            var result = await _recipesService.SaveAsync(recipe.Result);
            
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            
            var recipeResource = _mapper.Map<Recipe, RecipeResource>(result.Resource);
            return Ok(recipeResource);
        }
    }
}