using eOkruh.Common;

namespace eOkruh.Domain.MilitaryStructures
{
    public class Structure
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SpecialProperty { get; set; } = Strings.noData;

        public bool HasEmptyFields()
        {
            return string.IsNullOrWhiteSpace(Name) 
                || string.IsNullOrWhiteSpace(Type)
                || string.IsNullOrWhiteSpace(SpecialProperty);
        }
        public bool IsBranch()
        {
            return Type.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Branch]);
        }
        public bool IsPlatoon()
        {
            return Type.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Platoon]);
        }
        public bool IsCompany()
        {
            return Type.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Company]);
        }
        public bool IsBase()
        {
            return Type.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Base]);
        }
    }
}
