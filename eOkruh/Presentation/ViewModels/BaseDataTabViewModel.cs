using CommunityToolkit.Mvvm.ComponentModel;
using eOkruh.Common.UserManagement;

namespace eOkruh.Presentation.ViewModels
{
    public partial class BaseDataTabViewModel : ObservableObject
    {
        private readonly User user;

        public BaseDataTabViewModel(User user)
        {
            this.user = user;
        }
    }
}
