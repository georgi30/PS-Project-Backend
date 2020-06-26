using Persistence.Entities;

namespace PS_Project_Model.Responses
{
    public class AttachmentsResponse : BaseResponse<Attachment>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="attachment">Saved attachment.</param>
        /// <returns>Response.</returns>
        public AttachmentsResponse(Attachment attachment) : base(attachment)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AttachmentsResponse(string message) : base(message)
        { }
    }
}