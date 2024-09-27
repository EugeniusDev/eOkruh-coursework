using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
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
        bool isInEditingMode = false;
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
            if (IsInEditingMode)
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

        bool IsSecondarySearchBarRequired()
        {
            return SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[1]);
        }

        bool IsSecondarySearchBarEmpty()
        {
            return string.IsNullOrWhiteSpace(SecondarySearchBar);
        }

        async Task SearchForAllPersonnelInfos()
        {
            PersonnelInfos = await PersonnelManager.GetAllInfos();
        }
        async Task SearchForRank()
        {
            string rank = PrimarySearchBar.Trim();
            PersonnelInfos = await PersonnelManager.GetInfosByRank(rank);
        }
        async Task SearchForRankWithStructureScope()
        {
            string rank = PrimarySearchBar.Trim();
            string structureName = SecondarySearchBar.Trim();
            PersonnelInfos = await PersonnelManager
                .GetScopedInfosWithRank(rank, structureName);
        }

        async Task SearchForSpecialityWithStructureScope()
        {
            string speciality = PrimarySearchBar.Trim();
            string structureName = SecondarySearchBar.Trim();
            PersonnelInfos = await PersonnelManager
                .GetScopedInfosWithSpeciality(speciality, structureName);
        }
        #endregion

        #region Editing
        [RelayCommand]
        async Task ToggleEditMode()
        {
            if (IsInEditingMode)
            {
                SaveChangesErrorMessage = string.Empty;
                foreach (FullPersonnelInfo info in PersonnelInfos)
                {
                    if (!PersonnelManager
                        .IsMilitaryPersonInfoValid(info.MilitaryPerson, 
                            out string errorMessage))
                    {
                        SaveChangesErrorMessage = errorMessage;
                        return;
                    }
                    if (!await StructureManager.IsStructureStringValid(info))
                    {
                        SaveChangesErrorMessage = "Перевірте правильність написання структур під командуванням. " +
                            "Зауважте, що військовослужбовці рядового й сержантського складу можуть командувати " +
                            "тільки взводом і відділенням. " +
                            $"У випадку відсутності таких структур напишіть \"{Strings.noData}\"";
                        return;
                    }
                    if (!await StructureManager.StructureExists(new() { Name = info.MilitaryBase }))
                    {
                        SaveChangesErrorMessage = "Введено неіснуючу військову частину," +
                            "перевірте правильність написання";
                        return;
                    }
                }
                try
                {
                    foreach (FullPersonnelInfo info in PersonnelInfos)
                    {
                        await PersonnelManager.SavePersonnelInfo(info);
                    }
                }
                catch (Exception ex)
                {
                    SaveChangesErrorMessage = ex.Message;
                    return;
                }
                EditButtonText = Strings.edit;
                IsInEditingMode = false;
            }
            else
            {
                EditButtonText = Strings.save;
                IsInEditingMode = true;
            }
        }
        #endregion

        #region Adding new personnel
        [RelayCommand]
        async Task SaveNewPersonnelMember()
        {
            AddNewPersonnelErrorMessage = string.Empty;
            if (IsInEditingMode)
            {
                AddNewPersonnelErrorMessage = "Збережіть всі зміни, перш ніж " +
                    "додавати нові дані до бази";
                return;
            }
            if (!PersonnelManager
                .IsMilitaryPersonInfoValid(NewPersonnelMemberInfo.MilitaryPerson, 
                    out string errorMessage))
            {
                AddNewPersonnelErrorMessage = errorMessage;
                return;
            }
            if (!await StructureManager.IsStructureStringValid(NewPersonnelMemberInfo))
            {
                SaveChangesErrorMessage = "Перевірте правильність написання структур під командуванням. " +
                    "Зауважте, що військовослужбовці рядового й сержантського складу можуть командувати " +
                    "тільки взводом і відділенням. " +
                    $"У випадку відсутності таких структур напишіть \"{Strings.noData}\"";
                return;
            }
            if (!await StructureManager.StructureExists(
                    new() { Name = NewPersonnelMemberInfo.MilitaryBase }))
            {
                AddNewPersonnelErrorMessage = "Введено неіснуючу військову частину," +
                    "перевірте правильність написання";
                return;
            }
            try
            {
                await PersonnelManager.SavePersonnelInfo(NewPersonnelMemberInfo);
            }
            catch (Exception ex)
            {
                AddNewPersonnelErrorMessage = ex.Message;
            }
        }
        #endregion

        [RelayCommand]
        async static Task DeleteDatabase()
        {
            await NeoDeleter.DeleteMainDatabase();
        }
    }
}
