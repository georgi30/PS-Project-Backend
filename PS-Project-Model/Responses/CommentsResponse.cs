using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class CommentsResponse : BaseResponse<Comment>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="comment">Saved comment.</param>
        /// <returns>Response.</returns>
        public CommentsResponse(Comment comment) : base(comment)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CommentsResponse(string message) : base(message)
        { }
    }
}