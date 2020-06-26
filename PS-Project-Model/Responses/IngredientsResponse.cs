using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class IngredientsResponse : BaseResponse<Ingredient>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="ingredient">Saved ingredient.</param>
        /// <returns>Response.</returns>
        public IngredientsResponse(Ingredient ingredient) : base(ingredient)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public IngredientsResponse(string message) : base(message)
        { }
    }
}