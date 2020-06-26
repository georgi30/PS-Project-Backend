using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class NutritionFactsResponse : BaseResponse<NutritionFacts>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="facts">Saved facts.</param>
        /// <returns>Response.</returns>
        public NutritionFactsResponse(NutritionFacts facts) : base(facts)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public NutritionFactsResponse(string message) : base(message)
        { }
    }
}