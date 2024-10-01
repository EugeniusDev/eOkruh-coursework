using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using System.Collections.ObjectModel;

namespace eOkruh.Presentation.ViewModels
{
    public partial class StructuresTabViewModel : ObservableObject
    {
        [ObservableProperty]
        List<string> searchTypePickerItems =
        [
            "Пошук місця(місць) дислокації за назвою структури",                         //0r
            "Пошук структури з найбільшою кількістю в/ч (дивізія/корпус/бригада/армія)",//1
            "Пошук структури з найменшою кількістю в/ч (дивізія/корпус/бригада/армія)",//2
            "Вивід усіх в/ч та їх керівників",                                        //3
            "Пошук в/ч, що входять до вказаної структури та їх керівників",          //4r
            "Вивід споруд, де дислокується більше одного підрозділу",               //5
            "Вивід споруд, де не дислокується жодного підрозділу",                 //6
            "Вивід всіх структур"                                                 //7
        ];
        [ObservableProperty]
        string searchTypePickerSelectedItem = string.Empty;

        [ObservableProperty]
        string structureSearchBar = string.Empty;

        [ObservableProperty]
        string searchErrorMessage = string.Empty;

        [ObservableProperty]
        ObservableCollection<Structure> structures = [];
        [ObservableProperty]
        ObservableCollection<StructuresTab3PropsDto> threePropDtos = [];
        [ObservableProperty]
        StructuresTab3PropsDto threeColumnGridHeaders = new();

        [ObservableProperty]
        bool threeColumnGridActive = false;
        [ObservableProperty]
        bool allStructuresGridActive = true;
        
        [ObservableProperty]
        bool canEdit = false;
        [ObservableProperty]
        string editButtonText = Strings.edit;
        [ObservableProperty]
        bool isInEditingMode = false;
        [ObservableProperty]
        string saveChangesErrorMessage = string.Empty;

        [ObservableProperty]
        bool canAddNewStructures = false;
        [ObservableProperty]
        Structure newStructure = new();
        [ObservableProperty]
        Structure parentForNewStructure = new();
        [ObservableProperty]
        string addNewStructureErrorMessage = string.Empty;

        [ObservableProperty]
        bool canDeleteDatabase = false;

        public StructuresTabViewModel(User user)
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
                CanAddNewStructures = true;
            }
            else if (user.IsOwner())
            {
                CanAddNewStructures = true;
                CanDeleteDatabase = true;
            }
        }

        #region Search
        [RelayCommand]
        async Task PerformSearch()
        {
            SearchErrorMessage = string.Empty;
            if (IsInEditingMode)
            {
                SearchErrorMessage = "Збережіть зміни, перш ніж шукати інформацію";
                return;
            }

            Make3ColumnGridActive(); 
            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[1]))
            {
                await SearchForStructureWithMostBases();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[2]))
            {
                await SearchForStructureWithLeastBases();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[3]))
            {
                await SearchForAllBasesWithCommmanders();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[5]))
            {
                await SearchForBusyAddresses();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[6]))
            {
                await SearchForFreeAddresses();
                return;
            }
            else if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[7]))
            {
                MakeAllStructuresGridActive();
                await SearchForAllStructures();
                return;
            }

            if (!AreSearchRequiredFieldsFilled())
            {
                return;
            }

            if (SearchTypePickerSelectedItem.Equals(SearchTypePickerItems[0]))
            {
                await SearchForAddressInfoByStructureName(StructureSearchBar.Trim());
            }
            else
            {
                await SearchForChildBasesWithCommanders(StructureSearchBar.Trim());
            }
        }

        private void Make3ColumnGridActive()
        {
            ThreeColumnGridActive = true;
            AllStructuresGridActive = false;
        }
        private void MakeAllStructuresGridActive()
        {
            ThreeColumnGridActive = false;
            AllStructuresGridActive = true;
        }

        private async Task SearchForStructureWithMostBases()
        {
            ThreeColumnGridHeaders = new()
            { 
                Prop1 = "Назва",
                Prop2 = "Тип",
                Prop3 = "Кількість в/ч"
            };
            ThreePropDtos = await StructureManager.SearchForStructureWithMostBases();
        }

        private async Task SearchForStructureWithLeastBases()
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Назва",
                Prop2 = "Тип",
                Prop3 = "Кількість в/ч"
            };
            ThreePropDtos = await StructureManager.SearchForStructureWithLeastBases();
        }

        private async Task SearchForAllBasesWithCommmanders()
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Назва в/ч",
                Prop2 = "Місце дислокації",
                Prop3 = "Керівник (ПІБ)"
            };
            ThreePropDtos = await StructureManager.SearchForAllBasesWithCommmanders();
        }

        private async Task SearchForBusyAddresses()
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Назва в/ч",
                Prop2 = "Місце дислокації",
                Prop3 = "Кількість підрозділів"
            };
            ThreePropDtos = await StructureManager.SearchForBusyAddresses();
        }

        private async Task SearchForFreeAddresses()
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Назва в/ч",
                Prop2 = "Місце дислокації",
                Prop3 = "Кількість підрозділів"
            };
            ThreePropDtos = await StructureManager.SearchForFreeAddresses();
        }

        private async Task SearchForAllStructures()
        {
            Structures = await StructureManager.SearchForAllStructures();
        }

        bool AreSearchRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(SearchTypePickerSelectedItem))
            {
                SearchErrorMessage = "Будь ласка, оберіть тип пошуку";
                return false;
            }
            if (string.IsNullOrWhiteSpace(StructureSearchBar))
            {
                SearchErrorMessage = "Будь ласка, заповніть поле назви структури";
                return false;
            }

            return true;
        }

        private async Task SearchForAddressInfoByStructureName(string structureName)
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Місце дислокації",
                Prop2 = "Назва",
                Prop3 = "Тип"
            };
            try
            {
                ThreePropDtos = await StructureManager.
                    SearchForAddressInfoByStructureName(structureName);
            }
            catch (Exception ex)
            {
                SearchErrorMessage = ex.Message;
            }
        }

        private async Task SearchForChildBasesWithCommanders(string structureName)
        {
            ThreeColumnGridHeaders = new()
            {
                Prop1 = "Назва в/ч",
                Prop2 = "Місце дислокації",
                Prop3 = "Керівник (ПІБ)"
            };
            try
            {
                ThreePropDtos = await StructureManager.
                    SearchForChildBasesWithCommanders(structureName);
            }
            catch (Exception ex)
            {
                SearchErrorMessage = ex.Message;
            }
        }
        #endregion

        #region Editing
        [RelayCommand]
        async Task ToggleEditMode()
        {
            if (!CanEdit)
            {
                SaveChangesErrorMessage = "У Вас немає доступу до редагування структур";
                return;
            }

            if (IsInEditingMode)
            {
                SaveChangesErrorMessage = string.Empty;
                foreach (Structure structure in Structures)
                {
                    if (!StructureManager.IsTypeValid(structure.Type))
                    {
                        SaveChangesErrorMessage = $"Тип структури \"{structure.Type}\" " +
                            $"не існує. Вкажіть коректний тип";
                        return;
                    }
                }
                try
                {
                    foreach (Structure structure in Structures)
                    {
                        await StructureManager.SaveStructure(structure);
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

        #region Adding new structure
        [RelayCommand]
        async Task SaveNewStructure()
        {
            AddNewStructureErrorMessage = string.Empty;
            if (IsInEditingMode)
            {
                AddNewStructureErrorMessage = "Збережіть всі зміни, перш ніж " +
                    "додавати нові дані до бази";
                return;
            }
            if (NewStructure.HasEmptyFields())
            {
                AddNewStructureErrorMessage = "Заповніть всі потрібні поля " +
                    "для створення нової структури";
                return;
            }
            if (!await StructureManager.StructureExists(ParentForNewStructure))
            {
                AddNewStructureErrorMessage = "Введено неіснуючу батьківську структуру, " +
                    "перевірте правильність написання";
                return;
            }
            try
            {
                await StructureManager.SaveStructure(NewStructure);
                await NeoRelationManager
                    .MakeStructureInStructure(NewStructure, ParentForNewStructure);
                NewStructure = new();
            }
            catch (Exception ex)
            {
                AddNewStructureErrorMessage = ex.Message;
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
