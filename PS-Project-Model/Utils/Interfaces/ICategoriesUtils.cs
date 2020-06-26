using System.Threading.Tasks;
using Persistence.Entities;
using PS_Project_Model.Resources.Categories;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface ICategoriesUtils
    {
        Category ReadyForCreation(SaveCategoryResources resource);
        Task<CategoryResource> DeleteByUpdate(Category category);
    }
}