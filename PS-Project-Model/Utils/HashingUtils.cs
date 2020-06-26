using Newtonsoft.Json;

namespace PS_Project_Model.Utils
{
    public class HashingUtils
    {
        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public  string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public  bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
        
        public string AsJson<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}