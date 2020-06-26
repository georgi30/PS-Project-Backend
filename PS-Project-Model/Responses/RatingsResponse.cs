using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class RatingsResponse : BaseResponse<Rating>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="rating">Saved rating.</param>
        /// <returns>Response.</returns>
        public RatingsResponse(Rating rating) : base(rating)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RatingsResponse(string message) : base(message)
        { }
    }
}