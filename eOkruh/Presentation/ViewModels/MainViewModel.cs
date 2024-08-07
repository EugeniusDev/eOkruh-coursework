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
        PersonnelSearchViewModel? personnelVm;
        [ObservableProperty]
        BasePropertySearchViewModel? basePropertyVm;
        [ObservableProperty]
        BaseDataSearchViewModel? baseDataVm;
        [ObservableProperty]
        UsersTabViewModel? usersVm;

        public void ConfigureViewModel()
        {
            PersonnelVm = new();
            BasePropertyVm = new();
            BaseDataVm = new();
            UsersVm = new(User);
        }
    }
}
