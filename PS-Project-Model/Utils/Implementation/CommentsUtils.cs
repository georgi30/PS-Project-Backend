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
using PS_Project_Model.Resources.Comments;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project_Model.Utils.Implementation
{
    public class CommentsUtils : ICommentsUtils
    {
        private readonly ICommentsService _commentsService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IRequestUtils _requestUtils;
        private IRecipesService _recipesService;

        public CommentsUtils(ICommentsService commentsService, IMapper mapper, 
                             IRequestUtils requestUtils, IRecipesService recipesService,
                             IAuthService authService)
        {
            _commentsService = commentsService;
            _mapper = mapper;
            _requestUtils = requestUtils;
            _recipesService = recipesService;
            _authService = authService;
        }

        public async Task<Comment> ReadyForCreation(SaveCommentResource resource, string token)
        {
            int userId = token != null ? _requestUtils.GetUserIdFromToken(token) : 0;
            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            comment.WhoUserId = userId;
            
            return comment;
        }

        public async Task<CommentsResource> PrepareForListing(Comment comment)
        {
            var user = await _authService.GetUserById(comment.WhoUserId);
            var recipe = await _recipesService.GetAsync(comment.RecipeId);
            
            var commentsResource = _mapper.Map<Comment, CommentsResource>(comment);
            commentsResource.ApplicationUser = user;
            commentsResource.Recipe = recipe.Resource;

            return commentsResource;
        }
    }
}