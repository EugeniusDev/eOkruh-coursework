using eOkruh.Common;

namespace eOkruh.Domain.MilitaryStructures
{
    internal static class StructureTypeStringPairs
    {
        public static readonly Dictionary<StructureTypes, string> typeStrings = new()
        {
            { StructureTypes.Branch, "Відділення"},
            { StructureTypes.Platoon, "Взвод"},
            { StructureTypes.Company, "Рота"},
            { StructureTypes.Base, "Військова частина"},
            { StructureTypes.Division, "Дивізія"},
            { StructureTypes.Corps, "Корпус"},
            { StructureTypes.Brigade, "Бригада"},
            { StructureTypes.Army, "Армія"}
        };        

        public static readonly Dictionary<string, StructureTypes> types = new()
        {
            { "Відділення", StructureTypes.Branch},
            { "Взвод", StructureTypes.Platoon},
            { "Рота", StructureTypes.Company},
            { "Військова частина", StructureTypes.Base},
            { "Дивізія", StructureTypes.Division},
            { "Корпус", StructureTypes.Corps},
            { "Бригада", StructureTypes.Brigade},
            { "Армія", StructureTypes.Army}
        };
    }
}
