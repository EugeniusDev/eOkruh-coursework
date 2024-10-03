using eOkruh.Common;

namespace eOkruh.Domain.Property
{
    internal static class PropertyTypeRepresentations
    {
        #region Weapons
        public static readonly Dictionary<WeaponTypes, string> weaponStrings = new()
        {
            { WeaponTypes.AssaultRifle, "Штурмова гвинтівка" },
            { WeaponTypes.SniperRifle, "Снайперська гвинтівка" },
            { WeaponTypes.AntitankRocketComplex, "Протитанковий ракетний комплекс" },
            { WeaponTypes.GrenadeLauncher, "Ручний гранатомет" },
            { WeaponTypes.Mortar, "Міномет" },
            { WeaponTypes.Artillery, "Артилерійська установка" },
            { WeaponTypes.BallisticMissile, "Балістична ракета" },
            { WeaponTypes.WingedMissile, "Крилата ракета" },
            { WeaponTypes.ReactiveVolleyFireSystem, "Реактивна система залпового вогню" },
            { WeaponTypes.ShockUAV, "Ударний БПЛА" }
        };

        public static readonly Dictionary<string, WeaponTypes> weaponTypes = new()
        {
            { "Штурмова гвинтівка", WeaponTypes.AssaultRifle },
            { "Снайперська гвинтівка", WeaponTypes.SniperRifle },
            { "Протитанковий ракетний комплекс", WeaponTypes.AntitankRocketComplex },
            { "Ручний гранатомет", WeaponTypes.GrenadeLauncher },
            { "Міномет", WeaponTypes.Mortar },
            { "Артилерійська установка", WeaponTypes.Artillery },
            { "Балістична ракета", WeaponTypes.BallisticMissile },
            { "Крилата ракета", WeaponTypes.WingedMissile },
            { "Реактивна система залпового вогню", WeaponTypes.ReactiveVolleyFireSystem },
            {  "Ударний БПЛА", WeaponTypes.ShockUAV }
        };
        #endregion

        #region Equipment
        public static readonly Dictionary<EquipmentTypes, string> equipmentStrings = new()
        {
            { EquipmentTypes.Tank, "Танк" },
            { EquipmentTypes.ArmoredTransporter, "Бронетранспортер (БТР)" },
            { EquipmentTypes.InfantryBattleVehicle, "Бойова машина піхоти (БМП)" },
            { EquipmentTypes.EasyArmoredVehicle, "Легкоброньований автомобіль" },
            { EquipmentTypes.Helicopter, "Вертоліт" },
            { EquipmentTypes.FighterJet, "Винищувач" },
            { EquipmentTypes.ReconnaissanceUAV, "Розвідувальний БПЛА" },
            { EquipmentTypes.GroundUV, "Наземний БПА" },
            { EquipmentTypes.AntiAircraftMissileComplex, "Зенітно-ракетний комплекс (ЗРК)" }
        };

        public static readonly Dictionary<string, EquipmentTypes> equipmentTypes = new()
        {
            { "Танк", EquipmentTypes.Tank },
            { "Бронетранспортер (БТР)", EquipmentTypes.ArmoredTransporter },
            { "Бойова машина піхоти (БМП)", EquipmentTypes.InfantryBattleVehicle },
            { "Легкоброньований автомобіль", EquipmentTypes.EasyArmoredVehicle },
            { "Вертоліт", EquipmentTypes.Helicopter },
            { "Винищувач", EquipmentTypes.FighterJet },
            { "Розвідувальний БПЛА", EquipmentTypes.ReconnaissanceUAV },
            { "Наземний БПА", EquipmentTypes.GroundUV },
            { "Зенітно-ракетний комплекс (ЗРК)", EquipmentTypes.AntiAircraftMissileComplex }
        };
        #endregion
    }
}
