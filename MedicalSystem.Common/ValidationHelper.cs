using System.Text.RegularExpressions;

namespace MedicalSystem.Common
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string? emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return true;

            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            return Regex.IsMatch(emailAddress, pattern);
        }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber);
        }

        public static bool IsValidDateOfBirth(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
                return true;

            return dateOfBirth.Value <= DateTime.Today;
        }
    }
}
