using CommunityToolkit.Mvvm.ComponentModel;
using eOkruh.Common.UserManagement;

namespace eOkruh.Presentation.ViewModels
{
    [QueryProperty("User", nameof(User))]
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        User user;

        [ObservableProperty]
        PersonnelTabViewModel? personnelVm;
        [ObservableProperty]
        BasePropertyTabViewModel? basePropertyVm;
        [ObservableProperty]
        BaseDataTabViewModel? baseDataVm;
        [ObservableProperty]
        UsersTabViewModel? usersVm;

        public void ConfigureViewModel()
        {
            PersonnelVm = new(User);
            BasePropertyVm = new(User);
            BaseDataVm = new(User);
            UsersVm = new(User);
        }
    }
}
