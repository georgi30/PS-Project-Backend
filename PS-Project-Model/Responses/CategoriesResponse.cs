using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class CategoriesResponse : BaseResponse<Category>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved attachment.</param>
        /// <returns>Response.</returns>
        public CategoriesResponse(Category category) : base(category)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CategoriesResponse(string message) : base(message)
        { }
    }
}