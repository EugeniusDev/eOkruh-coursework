using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using System.Collections.ObjectModel;

namespace eOkruh.Domain.Property
{
    public static class PropertyManager
    {
        public static readonly List<string> allPropertyTypes =
        [
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.AssaultRifle],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.SniperRifle],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.AntitankRocketComplex],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.GrenadeLauncher],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.Mortar],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.Artillery],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.BallisticMissile],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.WingedMissile],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.ReactiveVolleyFireSystem],
            PropertyTypeRepresentations.weaponStrings[WeaponTypes.ShockUAV],

            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Tank],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ArmoredTransporter],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.InfantryBattleVehicle],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.EasyArmoredVehicle],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Helicopter],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.FighterJet],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ReconnaissanceUAV],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.GroundUV],
            PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.AntiAircraftMissileComplex],
        ];

        public static readonly Dictionary<string, (string, string)> specialPropertyInfoTuples = new()
        {
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.AssaultRifle],
                ("Калібр", "Дальність ефективної стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.SniperRifle],
                ("Калібр", "Дальність стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.AntitankRocketComplex],
                ("Пробиття броні", "Дальність стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.GrenadeLauncher],
                ("Калібр", "Дальність стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.Mortar],
                ("Калібр снарядів", "Дальність стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.Artillery],
                ("Калібр снарядів", "Дальність стрільби") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.BallisticMissile],
                ("Дальність польоту", "Вага бойової частини") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.WingedMissile],
                ("Дальність польоту", "Вага бойової частини") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.ReactiveVolleyFireSystem],
                ("Калібр снарядів", "Дальність ураження") },
            { PropertyTypeRepresentations.weaponStrings[WeaponTypes.ShockUAV],
                ("Вага бойової частини", "Час автономного польоту") },

            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Tank], 
                ("Броня", "Калібр гармати") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ArmoredTransporter], 
                ("Вага", "Дальність ходу") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.InfantryBattleVehicle], 
                ("Вага", "Озброєння") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.EasyArmoredVehicle], 
                ("Броня", "Швидкість") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Helicopter], 
                ("Озброєння", "Швидкість") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.FighterJet], 
                ("Максимальна швидкість", "Озброєння") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ReconnaissanceUAV], 
                ("Радіус дії", "Тривалість польоту") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.GroundUV], 
                ("Радіус дії", "Вантажопідйомність") },
            { PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.AntiAircraftMissileComplex], 
                ("Дальність ураження цілей", "Максимальна висота ураження") }
        };

        #region Search
        internal static async Task<ObservableCollection<PropertyDto>> SearchForAllEquipmentInfos()
        {
            var equipment = await NeoReader.GetAllEquipment();
            ObservableCollection<PropertyDto> equipmentInfos = [];
            foreach (Equipment item in equipment)
            {
                var masterStructures = await NeoRelationManager
                    .GetMasterStructuresFor(item);
                equipmentInfos.Add(new()
                {
                    Property = item,
                    AncestoryStructureName = Strings
                        .JoinWithComma(masterStructures.ToNames())
                });
            }

            return equipmentInfos;
        }

        internal static async Task<ObservableCollection<PropertyDto>> SearchForAllWeaponInfos()
        {
            var weapons = await NeoReader.GetAllWeapons();
            ObservableCollection<PropertyDto> weaponInfos = [];
            foreach (Weapon item in weapons)
            {
                var masterStructures = await NeoRelationManager
                    .GetMasterStructuresFor(item);
                weaponInfos.Add(new()
                {
                    Property = item,
                    AncestoryStructureName = Strings
                        .JoinWithComma(masterStructures.ToNames())
                });
            }

            return weaponInfos;
        }

        internal static async Task<ObservableCollection<string>> SearchForBaseNamesWithNoSuchEquipment(string equipmentType)
        {
            var containingBases = await GetContainingBaseNamesFor(new Equipment() { Type = equipmentType });
            var allBases = await NeoReader
                .GetStructuresOfType(StructureTypeStringPairs.typeStrings[StructureTypes.Base]);
            return [..allBases
                .Select(structure => structure.Name)
                .Where(milBase => !containingBases.Contains(milBase))];
        }

        private static async Task<IEnumerable<string>> GetContainingBaseNamesFor(Property property)
        {
            string propertyName = property is Weapon ? nameof(Weapon) : nameof(Equipment);
            var tuples = await NeoRelationManager.GetTuplesWithBasesAndOwnedPropertyCount(propertyName);
            return tuples
                .Where(t => t.Item2.Equals(property.Type))
                .Select(t => t.Item1);
        }
        internal static async Task<ObservableCollection<string>> SearchForBaseNamesWithNoSuchWeapons(string weaponType)
        {
            var containingBases = await GetContainingBaseNamesFor(new Weapon() { Type = weaponType });
            var allBases = await NeoReader
                .GetStructuresOfType(StructureTypeStringPairs.typeStrings[StructureTypes.Base]);
            return [..allBases
                .Select(structure => structure.Name)
                .Where(milBase => !containingBases.Contains(milBase))];
        }

        internal static async Task<ObservableCollection<PropertyDto>> SearchForEquipmentWithTypeIn(string equipmentType, string structureName)
        {
            if (!await GivenStructureCanHaveProperty(structureName))
            {
                throw new ArgumentException("Введеної структури не існує або вона не може мати власність. " +
                    "Будь ласка, вводьте назву існуючої структури з типом з переліку: відділення/взвод/рота");
            }
            ObservableCollection<Equipment> equipment = await NeoRelationManager
                .GetStructureOwnedEquipmentOfType(new() { Name = structureName }, equipmentType);
            ObservableCollection<PropertyDto> infos = [];
            foreach (Equipment item in equipment)
            {
                infos.Add(new()
                {
                    Property = item,
                    AncestoryStructureName = structureName
                });
            }

            return infos;
        }

        internal static async Task<bool> GivenStructureCanHaveProperty(string structureName)
        {
            if (await StructureManager.StructureExists(new() { Name = structureName }))
            {
                Structure structure = await NeoReader.GetStructure(structureName);
                return structure.CanHaveProperty();
            }

            return false;
        }

        internal static async Task<ObservableCollection<PropertyDto>> SearchForWeaponsWithTypeIn(string weaponsType, string structureName)
        {
            if (!await GivenStructureCanHaveProperty(structureName))
            {
                throw new ArgumentException("Введеної структури не існує або вона не може мати власність. " +
                    "Будь ласка, вводьте назву існуючої структури з типом з переліку: відділення/взвод/рота");
            }
            ObservableCollection<Weapon> weapons = await NeoRelationManager
                .GetStructureOwnedWeaponsOfType(new() { Name = structureName }, weaponsType);
            ObservableCollection<PropertyDto> infos = [];
            foreach (Weapon item in weapons)
            {
                infos.Add(new()
                {
                    Property = item,
                    AncestoryStructureName = structureName
                });
            }

            return infos;
        }
        internal static async Task<ObservableCollection<string>> SearchForBasesWithEquipmentOfTypeOfNumber(string equipmentType, int wantedCount)
        {
            var tuples = await NeoRelationManager.GetTuplesWithBasesAndOwnedPropertyCount(nameof(Equipment));
            return [..tuples
                .Where(t => t.Item2.Equals(equipmentType) && t.Item3 == wantedCount)
                .Select(t => t.Item1)];
        }
        internal static async Task<ObservableCollection<string>> SearchForBasesWithWeaponsOfTypeOfNumber(string weaponsType, int wantedCount)
        {
            var tuples = await NeoRelationManager.GetTuplesWithBasesAndOwnedPropertyCount(nameof(Weapon));
            return [..tuples
                .Where(t => t.Item2.Equals(weaponsType) && t.Item3 == wantedCount)
                .Select(t => t.Item1)];
        }

        #endregion

        #region Save/Delete
        internal static async Task SavePropertyInfo(PropertyDto newPropertyInfo)
        {
            Structure ancestoryStructure = await NeoReader
                .GetStructure(newPropertyInfo.AncestoryStructureName);
            if (PropertyTypeRepresentations.weaponStrings.ContainsValue(newPropertyInfo.Property.Type))
            {
                Weapon weapon = newPropertyInfo.Property.CopyToNewWeapon();
                await NeoSaver.SaveWeapon(weapon);
                await NeoRelationManager
                    .MakeHasProperty(ancestoryStructure, weapon);
            }
            else
            {
                Equipment equipment = newPropertyInfo.Property.CopyToNewEquipment();
                await NeoSaver.SaveEquipment(equipment);
                await NeoRelationManager
                    .MakeHasProperty(ancestoryStructure, equipment);
            }
        }
        public static async Task DeleteProperty(Property property)
        {
            if (PropertyTypeRepresentations.weaponStrings.ContainsValue(property.Type))
            {
                Weapon weapon = property.CopyToNewWeapon();
                await NeoDeleter.DeleteWeapon(weapon);
            }
            else
            {
                Equipment equipment = property.CopyToNewEquipment();
                await NeoDeleter.DeleteEquipment(equipment);
            }
        }
        #endregion
    }
}
