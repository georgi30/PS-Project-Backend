using System;

namespace PS_Project_Model.Resources.Auth
{
    public class RegisteredUserResource
    {
        public int UserId { get; set; }
        public short Active { get; set; }
        public short Deleted { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}