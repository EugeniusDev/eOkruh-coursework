using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Property;
using System.Collections.ObjectModel;

namespace eOkruh.Presentation.ViewModels
{
    public partial class PropertyTabViewModel : ObservableObject
    {
        [ObservableProperty]
        List<string> searchTypePickerItems =
        [
            "Вивід усієї військової техніки та структури, за якою вона приписана",//0
            "Пошук техніки вказаного типу у вказаній структурі",//1ps
            "Вивід усього військового озброєння та структури, за якою воно приписане",//2
            "Пошук озброєння вказаного типу у вказаній структурі",//3ps
            "Пошук військових частин, в яких знаходиться вказана кількість техніки вказаного типу",//4ps
            "Пошук військових частин, в яких немає жодної одиниці техніки вказаного типу",//5s
            "Пошук військових частин, в яких знаходиться вказана кількість озброєння вказаного типу",//6ps
            "Пошук військових частин, в яких немає жодної одиниці озброєння вказаного типу"//7s
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
        ObservableCollection<PropertyDto> propertyInfos = [];
        [ObservableProperty]
        ObservableCollection<string> milBasesNames = [];
        [ObservableProperty]
        bool mainGridActive = true;
        [ObservableProperty]
        bool basesGridActive = false;

        [ObservableProperty]
        bool canEdit = false;

        [ObservableProperty]
        bool canAddNewProperty = false;
        [ObservableProperty]
        PropertyDto newPropertyInfo = new();
        [ObservableProperty]
        string addNewPropertyErrorMessage = string.Empty;

        [ObservableProperty]
        bool canDeleteDatabase = false;

        public PropertyTabViewModel(User user)
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
                CanAddNewProperty = true;
            }
            else if (user.IsOwner())
            {
                CanAddNewProperty = true;
                CanDeleteDatabase = true;
            }
        }

        #region Search
        [RelayCommand]
        async Task PerformSearch()
        {
            SearchErrorMessage = string.Empty;
            if (!IsSearchTypeSelected())
            {
                return;
            }
            MakeMainGridActive();
            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[0]))
            {
                await SearchForAllEquipmentInfos();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[2]))
            {
                await SearchForAllWeaponInfos();
                return;
            }

            if (!IsSecondarySearchBarFilled())
            {
                return;
            }

            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[5]))
            {
                MakeBasesGridActive();
                await SearchForBasesWithNoSuchEquipment();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[7]))
            {
                MakeBasesGridActive();
                await SearchForBasesWithNoSuchWeapons();
                return;
            }

            if (!IsPrimarySearchBarFilled())
            {
                return;
            }

            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[1]))
            {
                await SearchForEquipmentWithTypeIn();
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[3]))
            {
                await SearchForWeaponsWithTypeIn();
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[4]))
            {
                MakeBasesGridActive();
                if (int.TryParse(PrimarySearchBar.Trim(), out int wantedCount))
                {
                    await SearchForBasesEquipmentOfTypeOfNumber(wantedCount);
                }
                else
                {
                    SearchErrorMessage = "Введіть число в поле кількості одиниць власності";
                }
            }
            else
            {
                MakeBasesGridActive();
                if (int.TryParse(PrimarySearchBar.Trim(), out int wantedCount))
                {
                    await SearchForBasesWithWeaponsOfTypeOfNumber(wantedCount);
                }
                else
                {
                    SearchErrorMessage = "Введіть число в поле кількості одиниць власності";
                }
            }
        }

        bool IsSearchTypeSelected()
        {
            if (string.IsNullOrWhiteSpace(SearchTypePickerSelectedItem))
            {
                SearchErrorMessage = "Будь ласка, оберіть тип пошуку";
                return false;
            }

            return true;
        }
        bool IsPrimarySearchBarFilled()
        {
            if (string.IsNullOrWhiteSpace(PrimarySearchBar))
            {
                SearchErrorMessage = "Будь ласка, заповніть поле назви структури/кількості одиниць власності";
                return false;
            }

            return true;
        }
        bool IsSecondarySearchBarFilled()
        {
            if (string.IsNullOrWhiteSpace(SecondarySearchBar))
            {
                SearchErrorMessage = "Будь ласка, заповніть поле конкретного типу власності";
                return false;
            }

            return true;
        }

        private void MakeMainGridActive()
        {
            MainGridActive = true;
            BasesGridActive = false;
        }
        private void MakeBasesGridActive()
        {
            MainGridActive = false;
            BasesGridActive = true;
        }

        async Task SearchForAllEquipmentInfos()
        {
            PropertyInfos = await PropertyManager.SearchForAllEquipmentInfos();
        }
        async Task SearchForAllWeaponInfos()
        {
            PropertyInfos = await PropertyManager.SearchForAllWeaponInfos();
        }

        async Task SearchForBasesWithNoSuchEquipment()
        {
            string eqType = SecondarySearchBar.Trim();
            if (!IsEquipmentTypeValid(eqType))
            {
                return;
            }
            MilBasesNames = await PropertyManager.SearchForBaseNamesWithNoSuchEquipment(eqType);
        }

        bool IsEquipmentTypeValid(string eqType)
        {
            if (!PropertyTypeRepresentations.equipmentStrings.ContainsValue(eqType))
            {
                SearchErrorMessage = "Введено неіснуючий тип техніки";
                return false;
            }
            return true;
        }
        bool IsWeaponTypeValid(string wpType)
        {
            if (!PropertyTypeRepresentations.weaponStrings.ContainsValue(wpType))
            {
                SearchErrorMessage = "Введено неіснуючий тип техніки";
                return false;
            }
            return true;
        }

        async Task SearchForBasesWithNoSuchWeapons()
        {
            string wpType = SecondarySearchBar.Trim();
            if (!IsWeaponTypeValid(wpType))
            {
                return;
            }

            MilBasesNames = await PropertyManager.SearchForBaseNamesWithNoSuchWeapons(wpType);
        }

        async Task SearchForEquipmentWithTypeIn()
        {
            string eqType = SecondarySearchBar.Trim();
            if (!IsEquipmentTypeValid(eqType))
            {
                return;
            }

            try
            {
                PropertyInfos = await PropertyManager
                    .SearchForEquipmentWithTypeIn(eqType, PrimarySearchBar.Trim());
            }
            catch (Exception ex)
            {
                SearchErrorMessage = ex.Message;
            }
        }
        async Task SearchForWeaponsWithTypeIn()
        {
            string wpType = SecondarySearchBar.Trim();
            if (!IsWeaponTypeValid(wpType))
            {
                return;
            }

            try
            {
                PropertyInfos = await PropertyManager
                    .SearchForWeaponsWithTypeIn(wpType, PrimarySearchBar.Trim());
            }
            catch (Exception ex)
            {
                SearchErrorMessage = ex.Message;
            }
        }

        async Task SearchForBasesEquipmentOfTypeOfNumber(int wantedCount)
        {
            string eqType = SecondarySearchBar.Trim();
            if (!IsEquipmentTypeValid(eqType))
            {
                return;
            }

            MilBasesNames = await PropertyManager.SearchForBasesWithEquipmentOfTypeOfNumber(eqType, wantedCount);
        }
        async Task SearchForBasesWithWeaponsOfTypeOfNumber(int wantedCount)
        {
            string wpType = SecondarySearchBar.Trim();
            if (!IsWeaponTypeValid(wpType))
            {
                return;
            }

            MilBasesNames = await PropertyManager.SearchForBasesWithWeaponsOfTypeOfNumber(wpType, wantedCount);
        }
        #endregion

        #region Editing
        [RelayCommand]
        async Task DeleteProperty(PropertyDto chosenProp)
        {
            await PropertyManager.DeleteProperty(chosenProp.Property);
            PropertyInfos.Remove(chosenProp);
        }
        #endregion

        #region Adding new property
        [RelayCommand]
        async Task SaveNewProperty()
        {
            AddNewPropertyErrorMessage = string.Empty;
            
            if (!await PropertyManager
                .GivenStructureCanHaveProperty(NewPropertyInfo.AncestoryStructureName))
            {
                AddNewPropertyErrorMessage = "Введено неіснуючу військову структуру або " +
                    "введена військова структура не може мати власність";
                return;
            }
            if (NewPropertyInfo.Property.HasEmptyFields())
            {
                AddNewPropertyErrorMessage = "Будь ласка, заповніть всі поля власності";
                return;
            }

            try
            {
                await PropertyManager.SavePropertyInfo(NewPropertyInfo);
                NewPropertyInfo = new();
            }
            catch (Exception ex)
            {
                AddNewPropertyErrorMessage = ex.Message;
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
