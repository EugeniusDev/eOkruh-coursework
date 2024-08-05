using eOkruh.Common.DataProcessing;
using System.Text.RegularExpressions;

namespace eOkruh.Common.UserManagement
{
    public static class UserManager
    {
        public static bool IsLoginValid(string login)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string phonePattern = @"^\+380\d{9}$";

            bool isEmail = Regex.IsMatch(login, emailPattern);
            bool isPhoneNumber = Regex.IsMatch(login, phonePattern);
            return isEmail || isPhoneNumber;
        }

        public static async Task<User?> RetrieveValidUser(string login, string password)
        {
            return await DatabaseReader.GetUserByLoginAndPassword(login, password);
        }

        public static async Task ResetUserPassword(string login, string newPassword)
        {
            await DatabaseSaver.SetNewPassword(login, newPassword);
        }
    }
}
