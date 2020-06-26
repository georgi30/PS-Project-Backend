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
using PS_Project_Model.Resources.Ratings;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class RatingsUtils : IRatingsUtils
    {
        private readonly IRatingsService _ratingsService;
        private readonly IRecipesService _recipesService;
        private readonly IMapper _mapper;
        private static Random random = new Random();

        public RatingsUtils(IRatingsService ratingsService, IRecipesService recipesService, IMapper mapper)
        {
            _ratingsService = ratingsService;
            _recipesService = recipesService;
            _mapper = mapper;
        }

        public async Task<Rating> ReadyForCreation(SaveRatingResource resource, int recipeId)
        {
            var result = await _recipesService.GetAsync(recipeId);

            if (!result.Success)
            {
                return null;
            }

            return new Rating
            {
                Score = resource.Rating,
                Recipe = result.Resource
            };
        }

        public async Task<RatingResource> DeleteByUpdate(Rating rating)
        {
            //await _ratingsService.UpdateAsync(rating.Rating, category);

            return _mapper.Map<Rating, RatingResource>(rating);
        }
    }
}