using eOkruh.Presentation.ViewModels;

namespace eOkruh
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private void EyeButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.IsPassword)
            {
                PasswordEntry.IsPassword = false;
                NewPasswordEntry.IsPassword = false;
                ConfirmNewPasswordEntry.IsPassword = false;
            }
            else
            {
                PasswordEntry.IsPassword = true;
                NewPasswordEntry.IsPassword = true;
                ConfirmNewPasswordEntry.IsPassword = true;
            }
        }

        private void ForgotPassword_Tapped(object sender, TappedEventArgs e)
        {
            Label forgotPasswordLabel = (Label)sender;
            forgotPasswordLabel.IsVisible = false;
            forgotPasswordLabel.IsEnabled = false;

            PasswordEntry.Text = string.Empty;

            PasswordLayout.IsEnabled = false;
            PasswordLayout.IsVisible = false;

            ForgotPasswordLayout.IsEnabled = true;
            ForgotPasswordLayout.IsVisible = true;
        }
    }

}
