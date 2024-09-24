using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Personnel;

namespace eOkruh.Domain.MilitaryStructures
{
    public static class StructureManager
    {
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
                if (await StructureExists(structure))
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

        public static async Task<bool> StructureExists(string structureName)
        {
            structureName = structureName.Trim();
            try
            {
                if (await DatabaseReader.GetStructure(structureName) is null)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
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

        private static async Task<string> GetStructureType(string structureName)
        {
            Structure structure = await DatabaseReader.GetStructure(structureName);
            return structure.Type;
        }

        public static async Task UpdateStructure(Structure oldStructure, Structure newStructure)
        {
            await DatabaseSaver.UpdateStructure(oldStructure, newStructure);
        }

        public static async Task<Structure> GetRelatedStructureWithType(Structure baseStructure, string wantedName, string wantedType)
        {
            throw new NotImplementedException();
            // TODO somehow recursively search for wanted structure
        }
    }
}
