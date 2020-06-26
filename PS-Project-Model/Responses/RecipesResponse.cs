using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class RecipesResponse : BaseResponse<Recipe>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="recipe">Saved recipe.</param>
        /// <returns>Response.</returns>
        public RecipesResponse(Recipe recipe) : base(recipe)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RecipesResponse(string message) : base(message)
        { }
    }
}