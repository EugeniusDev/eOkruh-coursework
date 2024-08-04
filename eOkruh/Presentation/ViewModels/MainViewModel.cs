using CommunityToolkit.Mvvm.ComponentModel;
using eOkruh.Common;
using eOkruh.Common.UserManagement;

namespace eOkruh.Presentation.ViewModels
{
    [QueryProperty("User", nameof(User))]
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        User user;

        [ObservableProperty]
        bool isEditingPermitted = false;

        [ObservableProperty]
        bool usersAccessPermitted = false;

        [ObservableProperty]
        bool canCreateAdmins = false;

        public void ConfigureViewModel()
        {
            // TODO implement, define what should be displayed due to access level
            var roles = RolesRepresentations.roleStrings;
            if (User.UserRole.Equals(roles[UserRoles.Operator]))
            {
                IsEditingPermitted = true;
            }
            else if (User.UserRole.Equals(roles[UserRoles.Administrator]))
            {
                IsEditingPermitted = true;
                UsersAccessPermitted = true;
            }
            else if (User.UserRole.Equals(roles[UserRoles.Owner]))
            {
                IsEditingPermitted = true;
                UsersAccessPermitted = true;
                CanCreateAdmins = true;
            }
        }
    }
}
