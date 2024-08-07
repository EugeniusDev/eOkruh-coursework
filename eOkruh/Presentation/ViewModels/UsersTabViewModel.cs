using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Common.UserManagement;
using System.Collections.ObjectModel;

namespace eOkruh.Presentation.ViewModels
{
    public partial class UsersTabViewModel : ObservableObject
    {
        private static readonly Dictionary<UserRoles, string> accessInfoMessages = new()
        {
            { UserRoles.Viewer, "Оскільки Ви переглядач, Ви не маєте " +
                "права створювати користувачів бази даних, а також не " +
                "маєте доступу до їхніх даних" },
            { UserRoles.Operator, "Оскільки Ви оператор, Ви не маєте " +
                "права створювати користувачів бази даних, а також не " +
                "маєте доступу до їхніх даних" },
            { UserRoles.Administrator, "Оскільки Ви адміністратор, Ви маєте право " +
                "призначати нових глядачів/операторів, але не " +
                "маєте доступу до даних користувачів бази даних" },
            { UserRoles.Owner, "Оскільки Ви власник, Ви маєте право " +
                "керувати даними всіх користувачів бази даних та " +
                "призначати нових адміністраторів" },
        };

        [ObservableProperty]
        User user;

        [ObservableProperty]
        string accessInfoMessage;

        [ObservableProperty]
        bool canAddUsers = false;

        [ObservableProperty]
        List<string> updatingUserRoleOptions;
        [ObservableProperty]
        string updatingUserRoleSelectedOption;
        [ObservableProperty]
        User userToUpdate = new();
        [ObservableProperty]
        string userUpdatingErrorMessage = string.Empty;

        [ObservableProperty]
        bool canManageUsers = false;
        [ObservableProperty]
        string fullInfoOutput = string.Empty;
        [ObservableProperty]
        ObservableCollection<FullUserInfo> fullUserInfos = [];

        public UsersTabViewModel(User currentUser)
        {
            User = currentUser;
            UpdatingUserRoleOptions = [string.Empty];
            UserRoles userRole = RolesRepresentations.roles[user.UserRole];
            AccessInfoMessage = accessInfoMessages[userRole];

            if (user.IsAdministrator())
            {
                CanAddUsers = true;
                UpdatingUserRoleOptions = [
                    RolesRepresentations.roleStrings[UserRoles.Viewer],
                        RolesRepresentations.roleStrings[UserRoles.Operator],
                    ];
            }
            else if (user.IsOwner())
            {
                CanAddUsers = true;
                UpdatingUserRoleOptions = [
                    RolesRepresentations.roleStrings[UserRoles.Administrator]
                ];
                CanManageUsers = true;
                PopulateFullUserInfos();
            }

            UpdatingUserRoleSelectedOption = UpdatingUserRoleOptions[0];
        }

        private async Task PopulateFullUserInfos()
        {
            FullUserInfos = await UserManager.GetAllFullUserInfos();
        }

        [RelayCommand]
        async Task TryUpdateUser()
        {
            // TODO fix. throws an error
            UserUpdatingErrorMessage = string.Empty;
            if (AreUserToUpdateFieldsFilled()
                && UserManager.IsLoginValid(UserToUpdate.Login))
            {
                UserToUpdate.UserRole = UpdatingUserRoleSelectedOption;
                await DatabaseSaver.SaveUserWithAssignee(UserToUpdate, User);
                if (User.IsOwner())
                {
                    await PopulateFullUserInfos();
                }
            }
        }

        bool AreUserToUpdateFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(UserToUpdate.FullName)
                || string.IsNullOrWhiteSpace(UserToUpdate.Login)
                || string.IsNullOrWhiteSpace(UserToUpdate.Password)
                || string.IsNullOrWhiteSpace(UpdatingUserRoleSelectedOption))
            {
                UserUpdatingErrorMessage = "Будь ласка заповніть всі потрібні поля " +
                    "та оберіть роль користувача";
                return false;
            }

            return true;
        }

        [RelayCommand]
        void OutputFullUserInfo(FullUserInfo fullUserInfo)
        {
            FullInfoOutput = fullUserInfo.ToString();
        }

        [RelayCommand]
        async Task DeleteUser(FullUserInfo fullUserInfo)
        {
            await DatabaseDeleter.DeleteUser(fullUserInfo.User);
            await PopulateFullUserInfos();
        }
    }
}
