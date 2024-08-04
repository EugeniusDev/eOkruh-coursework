using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common.UserManagement;
using eOkruh.Presentation.Pages;

namespace eOkruh.Presentation.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        bool passwordLayoutActive = true;
        [ObservableProperty]
        string login = string.Empty;
        [ObservableProperty]
        string password = string.Empty;

        [ObservableProperty]
        bool forgotPasswordLayoutActive = false;
        [ObservableProperty]
        string newPassword = string.Empty;
        [ObservableProperty]
        string confirmNewPassword = string.Empty;

        [ObservableProperty]
        bool isPasswordHidden = true;

        [ObservableProperty]
        string errorsOutput = string.Empty;

        [RelayCommand]
        void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
        }

        [RelayCommand]
        void ForgotPassword()
        {
            ForgotPasswordLayoutActive = true;
            PasswordLayoutActive = false;
        }

        [RelayCommand]
        async Task TryLogin()
        {
            ErrorsOutput = string.Empty;
            if (!IsLoginInputValid())
            {
                return;
            }

            if (IsPasswordInputValid())
            {
                await TryLogIntoMainPage();
            }
            else if (ForgotPasswordLayoutActive
                && IsForgotPasswordInputValid())
            {
                UserManager.ResetUserPassword(Login, NewPassword);
                Password = NewPassword; 
                await TryLogIntoMainPage();
            }
        }

        private bool IsLoginInputValid()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                ErrorsOutput = "Поле логіну порожнє, будь ласка, заповніть його";
                return false;
            }
            if (!UserManager.IsLoginValid(Login))
            {
                ErrorsOutput = "Введіть коректний логін, а саме Ваш номер телефону" +
                " чи Вашу електронну пошту";
                return false;
            }

            return true;
        }

        private bool IsPasswordInputValid()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorsOutput = "Поле паролю порожнє, будь ласка, заповніть його";
                return false;
            }

            return true;
        }

        private async Task TryLogIntoMainPage()
        {
            if (TryRetrieveUser(out User? user))
            {
                await Shell.Current.GoToAsync(
                    $"{nameof(MainPage)}?{nameof(User)}={user!}",
                    true,
                    new Dictionary<string, object>
                    {
                        {nameof(User), user!}
                    });
            }
        }

        private bool TryRetrieveUser(out User? user)
        {
            user = UserManager.RetrieveValidUser(Login, Password);
            if (user is null)
            {
                ErrorsOutput = "Користувача не існує. Перевірте правильність вводу";
                return false;
            }

            return true;
        }

        private bool IsForgotPasswordInputValid()
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                ErrorsOutput = "Поле нового паролю порожнє, будь ласка, " +
                    "заповніть його";
                return false;
            }
            if (string.IsNullOrWhiteSpace(ConfirmNewPassword))
            {
                ErrorsOutput = "Поле підтвердження нового паролю порожнє, " +
                    "будь ласка, заповніть його";
                return false;
            }
            if (!NewPassword.Equals(ConfirmNewPassword))
            {
                ErrorsOutput = "Паролі не збігаються. Будь ласка, введіть однаковий " +
                    "пароль для підтвердження створення нового";
                return false;
            }

            return true;
        }
    }
}
