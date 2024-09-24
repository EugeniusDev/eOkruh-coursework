using eOkruh.Common;

namespace eOkruh.Domain.MilitaryStructures
{
    public class Structure
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string SpecialProperty { get; set; } = Strings.noData;
        public string AncestorStructureName { get; set; } = Strings.noData;
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
        public bool IsBase()
        {
            return Type.Equals(StructureTypeStringPairs
                .typeStrings[StructureTypes.Base]);
        }
    }
}
