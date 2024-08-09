using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common.DataProcessing;
using eOkruh.Common.UserManagement;
using eOkruh.Domain.Personnel;
using System.Collections.ObjectModel;

namespace eOkruh.Presentation.ViewModels
{
    public partial class PersonnelTabViewModel : ObservableObject
    {
        [ObservableProperty]
        List<string> searchTypePickerItems =
        [
            "Пошук за званням",
            "Пошук за спеціальністю",
            "Вивід усіх військовослужбовців"
        ];
        [ObservableProperty]
        string searchTypePickerSelectedItem = string.Empty;

        [ObservableProperty]
        string primarySearchBar = string.Empty;
        [ObservableProperty]
        string secondarySearchBar = string.Empty;

        [ObservableProperty]
        string searchErrorMessage = string.Empty;

        [ObservableProperty]
        ObservableCollection<FullPersonnelInfo> personnelInfos = [];

        [ObservableProperty]
        bool canEdit = false;
        [ObservableProperty]
        string editButtonText = Strings.edit;
        [ObservableProperty]
        string saveChangesErrorMessage = string.Empty;

        [ObservableProperty]
        bool canAddNewPersonnel = false;
        [ObservableProperty]
        FullPersonnelInfo newPersonnelMemberInfo = new();
        [ObservableProperty]
        string addNewPersonnelErrorMessage = string.Empty;

        [ObservableProperty]
        bool canDeleteDatabase = false;

        public PersonnelTabViewModel(User user)
        {
            if (user.IsViewer())
            {
                return;
            }
            else
            {
                CanEdit = true;
            }

            if (user.IsAdministrator())
            {
                CanAddNewPersonnel = true;
            } 
            else if (user.IsOwner())
            {
                CanAddNewPersonnel = true;
                CanDeleteDatabase = true;
            }
        }

        #region Search
        [RelayCommand]
        async Task PerformSearch()
        {
            SearchErrorMessage = string.Empty;
            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[2]))
            {
                await SearchForAllPersonnelInfos();
                return;
            }

            if (!AreSearchRequiredFieldsFilled())
            {
                return;
            }
            if (IsEditModeActive())
            {
                SearchErrorMessage = "Збережіть зміни, перш ніж шукати інформацію";
            }

            if (IsSecondarySearchBarRequired())
            {
                if (IsSecondarySearchBarEmpty())
                {
                    SearchErrorMessage = "Введіть структуру, в межах якої потрібно здійснювати пошук";
                }
                else
                {
                    await SearchForSpecialityWithStructureScope();
                }
            }
            else
            {
                if (!RankExists())
                {
                    SearchErrorMessage = "Введено неіснуюче звання";
                    return;
                }

                if (IsSecondarySearchBarEmpty())
                {
                    await SearchForRank();
                }
                else
                {
                    await SearchForRankWithStructureScope();
                }
            }
        }

        bool AreSearchRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(SearchTypePickerSelectedItem))
            {
                SearchErrorMessage = "Будь ласка, оберіть тип пошуку";
                return false;
            }
            if (string.IsNullOrWhiteSpace(PrimarySearchBar))
            {
                SearchErrorMessage = "Будь ласка, заповніть поле звання/посади";
                return false;
            }

            return true;
        }

        private bool IsEditModeActive()
        {
            return EditButtonText.Equals(Strings.save);
        }

        bool IsSecondarySearchBarRequired()
        {
            return SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[1]);
        }

        bool IsSecondarySearchBarEmpty()
        {
            return string.IsNullOrWhiteSpace(SecondarySearchBar);
        }

        bool RankExists()
        {
            string rank = GetCorrectedRankInput();
            if (string.IsNullOrEmpty(rank))
            {
                return false;
            }

            return RankRepresentations.ordinary.ContainsKey(rank)
                || RankRepresentations.sergeant.ContainsKey(rank)
                || RankRepresentations.officer.ContainsKey(rank);
        }

        private string GetCorrectedRankInput()
        {
            string rank = PrimarySearchBar.Trim();
            if (rank.Length < 2)
            {
                return string.Empty;
            }
            rank = char.ToUpper(rank[0]) + rank[1..];

            return rank;
        }

        async Task SearchForAllPersonnelInfos()
        {
            try
            {
                PersonnelInfos = await PersonnelManager.GetAllInfos();
            }
            catch
            {
                SearchErrorMessage = "Неочікувана помилка. " +
                    "Нічого не знайдено";
            }
        }

        async Task SearchForRank()
        {
            string rank = GetCorrectedRankInput();
            try
            {
                PersonnelInfos = await PersonnelManager.GetInfosByRank(rank);
            }
            catch
            {
                SearchErrorMessage = "Нічого не знайдено. Перевірте " +
                    "правильність написання звання";
            }
        }

        async Task SearchForRankWithStructureScope()
        {
            string rank = GetCorrectedRankInput();
            string structureName = SecondarySearchBar.Trim();
            try
            {
                PersonnelInfos = await PersonnelManager
                    .GetInfosByRankWithScope(rank, structureName);
            }
            catch
            {
                SearchErrorMessage = "Нічого не знайдено. Перевірте " +
                    "правильність написання звання/структури";
            }
        }

        async Task SearchForSpecialityWithStructureScope()
        {
            string speciality = PrimarySearchBar.Trim();
            string structureName = SecondarySearchBar.Trim();
            try
            {
                PersonnelInfos = await PersonnelManager
                    .GetInfosBySpecialityAndStructure(speciality, structureName);
            }
            catch
            {
                SearchErrorMessage = "Нічого не знайдено. Перевірте " +
                    "правильність написання посади/структури";
            }
        }
        #endregion

        #region Editing
        [RelayCommand]
        void ToggleEditMode()
        {
            if (IsEditModeActive())
            {
                SaveChangesErrorMessage = string.Empty;
                // TODO maybe implement some checks
                try
                {
                    // TODO try to save everything

                }
                catch
                {
                    SaveChangesErrorMessage = "Неочікувана помилка при збереженні даних";
                    return;
                }
                EditButtonText = Strings.edit;
                // TODO make UI non-interactable
            }
            else
            {
                EditButtonText = Strings.save;
                // TODO make UI interactable

            }
        }
        #endregion

        #region Adding new personnel
        [RelayCommand]
        async Task SaveNewPersonnelMember()
        {
            AddNewPersonnelErrorMessage = string.Empty;
            if (IsEditModeActive())
            {
                AddNewPersonnelErrorMessage = "Збережіть всі зміни, перш ніж " +
                    "додавати нові дані до бази";
                return;
            }
            // TODO some checks and try to save that
        }
        #endregion

        [RelayCommand]
        async static Task DeleteDatabase()
        {
            await DatabaseDeleter.DeleteMainDatabase();
        }
    }
}
