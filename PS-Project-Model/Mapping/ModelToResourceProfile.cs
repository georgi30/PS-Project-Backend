using AutoMapper;
using Persistence.Entities;
using PS_Project_Model.Resources.Attachments;
using PS_Project_Model.Resources.Categories;
using PS_Project_Model.Resources.Comments;
using PS_Project_Model.Resources.Recipes;

namespace PS_Project_Model.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Recipe, RecipeResource>();
            CreateMap<Attachment, AttachmentsResource>();
            CreateMap<Comment, CommentsResource>();
            CreateMap<Category, CategoryResource>();
        }
    }
}