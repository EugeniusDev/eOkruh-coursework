using eOkruh.Presentation.ViewModels;

namespace eOkruh.Presentation.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
