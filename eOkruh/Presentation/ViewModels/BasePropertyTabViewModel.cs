using CommunityToolkit.Mvvm.ComponentModel;
using eOkruh.Common.UserManagement;

namespace eOkruh.Presentation.ViewModels
{
    public partial class BasePropertyTabViewModel : ObservableObject
    {
        private readonly User user;

        public BasePropertyTabViewModel(User user)
        {
            this.user = user;
        }
    }
}
