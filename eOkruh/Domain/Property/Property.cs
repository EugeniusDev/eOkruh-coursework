using eOkruh.Common;

namespace eOkruh.Domain.Property
{
    public abstract class Property
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SpecialProperty1 { get; set; } = Strings.noData;
        public string SpecialProperty2 { get; set; } = Strings.noData;
        public bool HasEmptyFields()
        {
            return string.IsNullOrWhiteSpace(Name)
                || string.IsNullOrWhiteSpace(Type)
                || string.IsNullOrWhiteSpace(SpecialProperty1)
                || string.IsNullOrWhiteSpace(SpecialProperty2);
        }
        public Weapon CopyToNewWeapon()
        {
            return new()
            {
                Name = Name,
                Type = Type,
                SpecialProperty1 = SpecialProperty1,
                SpecialProperty2 = SpecialProperty2
            };
        }
        public Equipment CopyToNewEquipment()
        {
            return new()
            {
                Name = Name,
                Type = Type,
                SpecialProperty1 = SpecialProperty1,
                SpecialProperty2 = SpecialProperty2
            };
        }
    }
}
