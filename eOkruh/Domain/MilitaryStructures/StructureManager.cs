using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Personnel;
using System.Collections.ObjectModel;

namespace eOkruh.Domain.MilitaryStructures
{
    public static class StructureManager
    {
        public static readonly List<string> structureTypeStrings =
        [
            StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
            StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
            StructureTypeStringPairs.typeStrings[StructureTypes.Company],
            StructureTypeStringPairs.typeStrings[StructureTypes.Base],
            StructureTypeStringPairs.typeStrings[StructureTypes.Division],
            StructureTypeStringPairs.typeStrings[StructureTypes.Corps],
            StructureTypeStringPairs.typeStrings[StructureTypes.Brigade],
            StructureTypeStringPairs.typeStrings[StructureTypes.Army]
        ];

        public static readonly List<string> baseContainingStructureTypes =
        [
            StructureTypeStringPairs.typeStrings[StructureTypes.Division],
            StructureTypeStringPairs.typeStrings[StructureTypes.Corps],
            StructureTypeStringPairs.typeStrings[StructureTypes.Brigade],
            StructureTypeStringPairs.typeStrings[StructureTypes.Army]
        ];


        public static readonly Dictionary<string, string> specialPropertyInfos = new()
        {
            { StructureTypeStringPairs.typeStrings[StructureTypes.Branch], "Оперативна назва" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Platoon], "Оперативна назва" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Company], "Місце дислокації" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Base], "Місце дислокації" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Division], "У складі сил (ЗСУ, НГУ...)" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Corps], "У складі сил (ЗСУ, НГУ...)" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Brigade], "У складі сил (ЗСУ, НГУ...)" },
            { StructureTypeStringPairs.typeStrings[StructureTypes.Army], "Додаткові примітки" },
        };

        public static bool IsTypeValid(string type)
        {
            return type.Length > 2 
                && structureTypeStrings.Contains(type.CapitalizeFirstLetter());
        }

        public static async Task<bool> IsStructureStringValid(FullPersonnelInfo info)
        {
            string structureString = info.StructuresUnderControl;
            if (structureString.Equals(Strings.noData))
            {
                return true;
            }

            List<string> structures = GetStructuresListFrom(structureString);
            foreach (string structure in structures)
            {
                if (await StructureExists(new() { Name = structure }))
                {
                    if (!info.MilitaryPerson.IsOfficer()
                        && !await CanBeControlledByNotOfficer(structure))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static List<string> GetStructuresListFrom(string structureString)
        {
            return [.. structureString.Split(Strings.separator,
                StringSplitOptions.RemoveEmptyEntries)];
        }

        public static async Task<bool> StructureExists(Structure structure)
        {
            return await NeoValidator.NodeExists(nameof(Structure),
                "Name", structure.Name);
        }

        private static async Task<bool> CanBeControlledByNotOfficer(string structureName)
        {
            string structureType;
            try
            {
                structureType = await GetStructureType(structureName);
            }
            catch
            {
                return false;
            }

            return structureType.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Branch])
                || structureType.Equals(StructureTypeStringPairs
                    .typeStrings[StructureTypes.Platoon]);
        }

        public static async Task<string> GetStructureType(string structureName)
        {
            Structure structure = await NeoReader.GetStructure(structureName);
            return structure.Type;
        }

        public static async Task SaveStructure(Structure structure)
        {
            await NeoSaver.SaveStructure(structure);
        }

        #region Search related
        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForStructureWithMostBases()
        {
            ObservableCollection<StructuresTab3PropsDto> mostBasesStructuresInfos = [];
            var infosLists = await GetBaseContainingStructuresInfos();
            foreach (var list in infosLists)
            {
                mostBasesStructuresInfos.Add(
                    list.OrderByDescending(item => int.Parse(item.Prop3))
                    .FirstOrDefault() ?? new()
                    );
            }
            return mostBasesStructuresInfos;
        }

        private static async Task<List<List<StructuresTab3PropsDto>>> GetBaseContainingStructuresInfos()
        {
            List<List<StructuresTab3PropsDto>> baseContainingStructuresInfos = [];
            int i = 0;
            foreach (string type in baseContainingStructureTypes)
            {
                List<StructuresTab3PropsDto> currentStructures = [];
                var typedStructures = await NeoReader.GetAllStructuresOfType(type);
                foreach (Structure structure in typedStructures)
                {
                    var info = await NeoReader.GetBasesCountInfoFor(structure);
                    currentStructures.Add(info);
                }
                baseContainingStructuresInfos.Add(currentStructures);
            }

            return baseContainingStructuresInfos;
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForStructureWithLeastBases()
        {
            ObservableCollection<StructuresTab3PropsDto> leastBasesStructuresInfos = [];
            var infosLists = await GetBaseContainingStructuresInfos();
            foreach (var list in infosLists)
            {
                leastBasesStructuresInfos.Add(
                    list.OrderBy(item => int.Parse(item.Prop3))
                    .FirstOrDefault() ?? new()
                    );
            }
            return leastBasesStructuresInfos;
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForAllBasesWithCommmanders()
        {
            var milBases = await NeoReader.GetAllStructuresOfType(StructureTypeStringPairs
                    .typeStrings[StructureTypes.Base]);
            ObservableCollection<StructuresTab3PropsDto> baseInfos = [];
            foreach (var baseStructure in milBases)
            {
                MilitaryPerson baseCommander = await NeoRelationManager
                    .GetBaseCommander(baseStructure);
                baseInfos.Add(new()
                {
                    Prop1 = baseStructure.Name,
                    Prop2 = baseStructure.SpecialProperty,
                    Prop3 = baseCommander.FullName
                });
            }
            return baseInfos;
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForBusyAddresses()
        {
            var addressesInfo = await GetBasesChildStructuresCountInfos();
            return [..addressesInfo.Where(info => !info.Prop3.Equals(Strings.zero))];
        }

        private static async Task<ObservableCollection<StructuresTab3PropsDto>> GetBasesChildStructuresCountInfos()
        {
            var milBases = await NeoReader
                .GetAllStructuresOfType(StructureTypeStringPairs
                    .typeStrings[StructureTypes.Base]);
            ObservableCollection<StructuresTab3PropsDto> baseInfos = [];
            foreach (var baseStructure in milBases)
            {
                var childStructures = await NeoRelationManager
                    .GetAllChildStructures(baseStructure);
                baseInfos.Add(new()
                {
                    Prop1 = baseStructure.Name,
                    Prop2 = baseStructure.SpecialProperty,
                    Prop3 = childStructures.Count.ToString()
                });
            }
            return baseInfos;
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForFreeAddresses()
        {
            var addressesInfo = await GetBasesChildStructuresCountInfos();
            return [.. addressesInfo.Where(info => info.Prop3.Equals(Strings.zero))];
        }

        public static async Task<ObservableCollection<Structure>> SearchForAllStructures()
        {
            return await NeoReader.GetAllStructures();
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForChildBasesWithCommanders(string structureName)
        {
            if (await CanStructureContainBases(structureName))
            {
                var childStructures = await NeoRelationManager
                    .GetAllChildStructures(new() { Name = structureName });
                string[] childStructureNames = childStructures.ToNames();

                var allBasesInfos = await SearchForAllBasesWithCommmanders();
                var childBasesInfos = allBasesInfos.Where(milBase => 
                    childStructureNames.Contains(milBase.Prop1));
                return [..childBasesInfos];
            }
            else 
            {
                throw new ArgumentException("Введена структура не містить військових частин. " +
                    "Введіть назву структури одного з типів: армія/бригада/корпус/дивізія");
            }
        }

        private static async Task<bool> CanStructureContainBases(string structureName)
        {
            Structure parentStructure = await NeoReader.GetStructure(structureName);
            return parentStructure.Type
                .Equals(StructureTypeStringPairs.typeStrings[StructureTypes.Army])
                || parentStructure.Type
                .Equals(StructureTypeStringPairs.typeStrings[StructureTypes.Brigade])
                || parentStructure.Type
                .Equals(StructureTypeStringPairs.typeStrings[StructureTypes.Corps])
                || parentStructure.Type
                .Equals(StructureTypeStringPairs.typeStrings[StructureTypes.Division]);
        }

        public static async Task<ObservableCollection<StructuresTab3PropsDto>> SearchForAddressInfoByStructureName(string structureName)
        {
            var structureToSearchFor = await NeoReader.GetStructure(structureName);
            ObservableCollection<StructuresTab3PropsDto> structureAddressInfos = [];
            if (HasItsOwnAddress(structureToSearchFor))
            {
                structureAddressInfos.Add(new()
                {
                    Prop1 = structureToSearchFor.SpecialProperty,
                    Prop2 = structureToSearchFor.Name,
                    Prop3 = structureToSearchFor.Type
                });
            }
            else if (await CanStructureContainBases(structureName))
            {
                var childBasesInfos = await SearchForChildBasesWithCommanders(structureName);
                foreach (StructuresTab3PropsDto childBaseInfo in childBasesInfos)
                {
                    string childBaseAddress = childBaseInfo.Prop2;
                    string childBaseName = childBaseInfo.Prop1;
                    structureAddressInfos.Add(new()
                    {
                        Prop1 = childBaseAddress,
                        Prop2 = childBaseName,
                        Prop3 = structureToSearchFor.Type
                    });
                }
            }
            else
            {
                Structure ancestoryStructure = await NeoRelationManager
                    .GetAncestoryStructureFor(structureToSearchFor);
                while (!ancestoryStructure.IsCompany())
                {
                    ancestoryStructure = await NeoRelationManager
                        .GetAncestoryStructureFor(ancestoryStructure);
                }

                structureAddressInfos.Add(new()
                {
                    Prop1 = ancestoryStructure.SpecialProperty,
                    Prop2 = structureToSearchFor.Name,
                    Prop3 = structureToSearchFor.Type
                });
            }
            
            return structureAddressInfos.Count != 0 ? structureAddressInfos 
                : throw new ArgumentException("Введена структура не приписана до жодної " +
                "з військових частин.");
        }

        private static bool HasItsOwnAddress(Structure currentStructure)
        {
            return currentStructure.IsCompany() || currentStructure.IsBase();
        }

        #endregion
    }
}
