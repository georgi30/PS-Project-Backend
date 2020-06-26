using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Persistence.Entities;
using PS_Project_Model.Resources;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Resources.Comments;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project.Controllers
{
    [Authorize]
    [Route("/api/comments")]
    [Produces("application/json")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly IMapper _mapper;
        private readonly ICommentsUtils _utils;

        public CommentsController(ICommentsService commentsService, IMapper mapper, 
            ICommentsUtils utils)
        {
            _commentsService = commentsService;
            _mapper = mapper;
            _utils = utils;
        }

        /// <summary>
        /// Lists all comments.
        /// </summary>
        /// <returns>List of comments.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CommentsResource>), 200)]
        public async Task<IEnumerable<CommentsResource>> ListAsync()
        {
            List<CommentsResource> resources = new List<CommentsResource>();
            
            var comments = await _commentsService.ListAsync();
            foreach (var comment in comments)
            {
                resources.Add(await _utils.PrepareForListing(comment));   
            }

            return resources;
        }

        /// <summary>
        /// Gets an existing comment according to an identifier.
        /// </summary>
        /// <param name="id">comment identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _commentsService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var commentsResource = await _utils.PrepareForListing(result.Resource);
            return Ok(commentsResource);
        }

        /// <summary>
        /// Saves a new comment.
        /// </summary>
        /// <param name="resource">Comment data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommentsResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromForm] SaveCommentResource resource)
        {
            
            var comment = await _utils.ReadyForCreation(resource, Request.Headers[HeaderNames.Authorization]);

            if (comment == null)
            {
                return BadRequest(new ErrorResource("Unauthorized access to comments!"));
            }
            
            var result = await _commentsService.SaveAsync(comment);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var commentsResource = _mapper.Map<Comment, CommentsResource>(result.Resource);
            return Ok(commentsResource);
        }

        /// <summary>
        /// Updates an existing comment according to an identifier.
        /// </summary>
        /// <param name="id">Comment identifier.</param>
        /// <param name="resource">Updated Comment data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
        {
            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _commentsService.UpdateAsync(id, comment);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var commentsResource = _mapper.Map<Comment, CommentsResource>(result.Resource);
            return Ok(commentsResource);
        }

        /// <summary>
        ///    Deletes a given comment according to an identifier.
        /// </summary>
        /// <param name="id">Comment identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommentsResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _commentsService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }


            var commentsResource = await _commentsService.DeleteAsync(result.Resource.CommentId);
            return Ok(commentsResource);
        }
        
        /// <summary>
        /// Lists all comments.
        /// </summary>
        /// <returns>List of comments.</returns>
        [HttpGet("getByRecipe/{recipeId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CommentsResource>), 200)]
        public async Task<IEnumerable<CommentsResource>> ListByPostAsync(int recipeId)
        {
            var resources = new List<CommentsResource>();
            var comments = await _commentsService.FindByRecipe(recipeId);
            
            foreach (var comment in comments)
            {
                resources.Add(await _utils.PrepareForListing(comment));   
            }
            
            return resources;
        }
    }
}