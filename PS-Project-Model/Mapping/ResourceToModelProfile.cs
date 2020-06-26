using AutoMapper;
using Persistence.Entities;
using PS_Project_Model.Resources.Attachments;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Resources.Comments;
using PS_Project_Model.Resources.Recipes;
using PS_Project_Model.Utils.Implementation;

namespace PS_Project_Model.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAttachmentsResource, Attachment>();
            CreateMap<SaveRecipeResource, Recipe>();
            CreateMap<SaveCategoryResources, Category>();
            CreateMap<UpdateRecipeResource, Recipe>();
            CreateMap<SaveCommentResource, Comment>();
        }
    }
}