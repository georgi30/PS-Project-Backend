using System;

namespace PS_Project_Model.Resources.Attachments
{
    public class AttachmentsResource
    {
        public int AttachmentId { get; set; }
        public short Active { get; set; } = 1;

        public short Deleted { get; set; } = 0;

        public string FilePath { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public string FileSlug { get; set; }
        public int? WhoUserId { get; set; }
    }
}