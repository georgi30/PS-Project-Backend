using System;
using System.Text.RegularExpressions;
using PS_Project_Model.Resources.Auth;

namespace PS_Project_Model.Validations
{
    public class AuthenticationValidation
    {
        private static readonly string EMAIL_PATTERN =
            @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        
        public static bool IsLoginValid(string email)
        {
            return email.Length != 0 && Regex.IsMatch(email, EMAIL_PATTERN);
        }

        public static bool IsRegisterValid(RegisterResource resource)
        {
            return resource.Email.Length != 0 && Regex.IsMatch(resource.Email, EMAIL_PATTERN) &&
                   resource.Password.Length != 0 && resource.ConfirmPassword.Length != 0 && resource.Password == resource.ConfirmPassword;
        }
    }
}