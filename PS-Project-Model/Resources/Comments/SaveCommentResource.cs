using System.ComponentModel.DataAnnotations;

namespace PS_Project_Model.Resources.Comments
{
    public class SaveCommentResource
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}