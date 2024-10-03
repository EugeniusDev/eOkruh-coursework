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
        StructuresTabViewModel? structuresVm;
        [ObservableProperty]
        PropertyTabViewModel? propertyVm;
        [ObservableProperty]
        UsersTabViewModel? usersVm;

        public void ConfigureViewModel()
        {
            PersonnelVm = new(User);
            StructuresVm = new(User);
            PropertyVm = new(User);
            UsersVm = new(User);
        }
    }
}
