using eOkruh.Common.DataProcessing;
using System.Collections.ObjectModel;
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

        public static async Task<User> RetrieveValidUserForLogin(string login, string password)
        {
            User? user = await NeoReader.GetUserByLoginAndPassword(login, password)
                ?? throw new ArgumentException("No such user found");
            await NeoSaver.UpdateLastLoginTime(user);
            user.SetCurrentDateOfLogin();
            return user;
        }

        public static async Task ResetUserPassword(string login, string newPassword)
        {
            await NeoSaver.SetNewPassword(login, newPassword);
        }

        public static async Task<ObservableCollection<FullUserInfo>> GetAllFullUserInfos()
        {
            List<string> fullNames = await NeoReader.GetAllUserFullNames();
            ObservableCollection<FullUserInfo> fullUserInfos = [];
            fullNames.ForEach(async fn => 
                fullUserInfos.Add(await NeoReader.GetFullUserInfo(fn))
                );
            return fullUserInfos;
        }
    }
}
