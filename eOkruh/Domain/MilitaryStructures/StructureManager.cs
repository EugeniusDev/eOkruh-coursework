using eOkruh.Common;
using eOkruh.Domain.Personnel;

namespace eOkruh.Domain.MilitaryStructures
{
    public static class StructureManager
    {
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
                throw new NotImplementedException();// TODO implement read in database

            }
            catch
            {
                return false;
            }

            return true;
        }

        private static async Task<bool> CanBeControlledByNotOfficer(string structureName)
        {
            string structureType = await GetStructureType(structureName);
            return structureType.Equals(StructureTypeStringPairs
                .typeStrings[Common.StructureTypes.Branch])
                || structureType.Equals(StructureTypeStringPairs
                    .typeStrings[Common.StructureTypes.Platoon]);
        }

        private static async Task<string> GetStructureType(string structureName)
        {
            throw new NotImplementedException();// todo retrieve structure's type only
        }
    }
}
