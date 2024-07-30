using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common.DataProcessing;
using System.Text.RegularExpressions;

namespace eOkruh.Presentation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string login = string.Empty;
        [ObservableProperty]
        string password = string.Empty;
        [ObservableProperty]
        string newPassword = string.Empty;
        [ObservableProperty]
        string confirmNewPassword = string.Empty;
        [ObservableProperty]
        string errorsOutput = string.Empty;

        [RelayCommand]
        void TryLogin()
        {
            ErrorsOutput = string.Empty;
            if (IsBasicInputValid())
            {
                if (DatabaseReader.IsLoginInfoValid(Login, Password))
                {
                    // TODO get full user info and navigate to main page
                }
            }
            else if (IsForgotPasswordInputValid())
            {

            }
        }

        private bool IsBasicInputValid()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                ErrorsOutput = "Поле логіну порожнє, будь ласка, заповніть його";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorsOutput = "Поле паролю порожнє, будь ласка, заповніть його";
                return false;
            }

            return IsLoginValid();
        }

        private bool IsLoginValid()
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string phonePattern = @"^\+380\d{9}$";

            bool isEmail = Regex.IsMatch(Login, emailPattern);
            bool isPhoneNumber = Regex.IsMatch(Login, phonePattern);
            if (isEmail || isPhoneNumber)
            {
               return true;
            }

            ErrorsOutput = "Введіть коректний логін, а саме Ваш номер телефону" +
                " чи Вашу електронну пошту";
            return false;
        }

        private bool IsForgotPasswordInputValid()
        {
            return !string.IsNullOrWhiteSpace(NewPassword)
                && !string.IsNullOrWhiteSpace(ConfirmNewPassword)
                && NewPassword.Equals(ConfirmNewPassword);
        }
    }
}
