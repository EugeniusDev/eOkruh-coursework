using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Personnel;
using Neo4j.Driver;
using System.Collections.ObjectModel;

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

        public static async Task<ObservableCollection<Structure>> GetAllChildStructures(Structure parentStructure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = @"
                MATCH (n1:Structure {Name: $parentStructureName})<-[:IS_PART_OF*]-(n2:Structure)
                RETURN n2.Name AS Name, n2.Type AS Type, n2.SpecialProperty AS SP";
            ObservableCollection<Structure> childStructures = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { parentStructureName = parentStructure.Name });
                await resultCursor.ForEachAsync(record =>
                {
                    childStructures.Add(new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SP"].As<string>()
                    });
                });
            });

            return childStructures;
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
            Structure structure = await NeoReader.GetStructure(structureName);
            return structure.Type;
        }

        public static async Task UpdateStructure(Structure oldStructure, Structure newStructure)
        {
            await NeoSaver.UpdateStructure(oldStructure, newStructure);
        }

        public static async Task<Structure> GetRelatedStructureWithType(Structure baseStructure, string wantedName, string wantedType)
        {
            throw new NotImplementedException();
            // TODO somehow recursively search for wanted parentStructure
        }
    }
}
