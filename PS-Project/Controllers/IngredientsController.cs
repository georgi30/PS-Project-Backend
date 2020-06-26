using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using PS_Project_Model.Resources.Others;
using PS_Project_Model.Services.Interfaces;

namespace PS_Project.Controllers
{
    [Route("/api/ingredients")]
    [Produces("application/json")]
    [ApiController]
    public class IngredientsController
    {
        private IIngredientsService _ingredientsService;
        private IMapper _mapper;

        public IngredientsController(IIngredientsService ingredientsService, IMapper mapper)
        {
            _ingredientsService = ingredientsService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Lists all ingredients.
        /// </summary>
        /// <returns>List of ingredients.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<IngredientResource>), 200)]
        public async Task<IEnumerable<IngredientResource>> ListAsync()
        {
            var ingredients = await _ingredientsService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientResource>>(ingredients);

            return resources;
        }
    }
}