using System.ComponentModel.DataAnnotations;

namespace PS_Project_Model.Resources.Categories
{
    public class SaveCategoryResources
    {
        [Required]
        public string Name { get; set; }
    }
}