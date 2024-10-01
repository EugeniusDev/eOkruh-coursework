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
            // Uncomment to refill database
            //await NeoDeleter.DeleteMainDatabase();
            //await NeoFiller.CreateSampleData();

            ErrorsOutput = string.Empty;
            if (!IsLoginInputValid())
            {
                return;
            }

            if (!ForgotPasswordLayoutActive && IsPasswordInputValid())
            {
                await TryLogIntoMainPage();
            }
            else if (ForgotPasswordLayoutActive
                && IsForgotPasswordInputValid())
            {
                try
                {
                    await UserManager.ResetUserPassword(Login, NewPassword);
                    Password = NewPassword;
                    await TryLogIntoMainPage();
                }
                catch
                {
                    ErrorsOutput = "Користувача не існує. Перевірте правильність вводу";
                }
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
            try
            {
                User user = await UserManager.RetrieveValidUserForLogin(Login, Password);
                await Shell.Current.GoToAsync(
                    $"{nameof(MainPage)}?{nameof(User)}={user!}",
                    true,
                    new Dictionary<string, object>
                    {
                        {nameof(User), user!}
                    });
            }
            catch
            {
                ErrorsOutput = "Користувача не існує. Перевірте правильність вводу";
            }
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
