using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;

namespace eOkruh.Common.DataProcessing
{
    static class DatabaseRelationMaker
    {
        public static async Task MakeRegisteredIn(MilitaryPerson person, Structure structure)
        {
            if (structure.IsBase())
            {
                await PersonToStructure(person, structure, "REGISTERED_IN");
            }
            else
            {
                throw new ArgumentException("Military personnel should be " +
                    "registered in military base");
            }
        }
        public static async Task MakeCommands(MilitaryPerson person, Structure structure)
        {
            if (person.IsOrdinary() && 
                !(structure.IsBranch() || structure.IsPlatoon()))
            {
                throw new ArgumentException("Ordinary personnel can command with " +
                    "only branches or platoons");
            }

            await PersonToStructure(person, structure, "COMMANDS");
        }
        
        private static async Task PersonToStructure(MilitaryPerson person, Structure structure, string relationType)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var query = @"
                MATCH (p:MilitaryPerson {FullName: $fullName}), (s:Structure {Name: $name})
                CREATE (p)-[r:" + relationType + "]->(s)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    fullName = person.FullName.Trim(),
                    name = structure.Name.Trim()
                });
            });
        }

        public static async Task StructureInStructure(Structure childStructure, Structure parentStructure)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var query = @"
                MATCH (s1:Structure {Name: $name1}), (s2:Structure {Name: $name2})
                CREATE (s1)-[r:IS_PART_OF]->(s2)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    name1 = childStructure.Name.Trim(),
                    name2 = parentStructure.Name.Trim()
                });
            });
        }
    }
}
