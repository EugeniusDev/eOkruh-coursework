using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using Neo4j.Driver;
using System.Collections.ObjectModel;

namespace eOkruh.Common.DataProcessing
{
    static class NeoRelationManager
    {
        #region Making relations
        public static async Task MakeRegisteredIn(MilitaryPerson person, Structure structure)
        {
            if (structure.IsBase())
            {
                await MakePersonToStructure(person, structure, NeoStrings.registeredInRelation);
            }
            else
            {
                throw new ArgumentException("Особовий склад повинен бути приписаним " +
                    "до певної військової частини");
            }
        }
        public static async Task MakeCommands(MilitaryPerson person, Structure structure)
        {
            if (person.IsOrdinary() && 
                !(structure.IsBranch() || structure.IsPlatoon()))
            {
                throw new ArgumentException("Рядовий особовий склад може командувати " +
                    "лише відділеннями та взводами");
            }

            await MakePersonToStructure(person, structure, NeoStrings.commandsRelation);
        }
        
        private static async Task MakePersonToStructure(MilitaryPerson person, Structure structure, string relationType)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (p:{nameof(MilitaryPerson)} {{FullName: $fullName}}), 
                    (s:{nameof(Structure)} {{Name: $name}})
                CREATE (p)-[r:{relationType}]->(s)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    fullName = person.FullName.Trim(),
                    name = structure.Name.Trim()
                });
            });
        }

        public static async Task MakeStructureInStructure(Structure childStructure, Structure parentStructure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s1:{nameof(Structure)} {{Name: $name1}}), (s2:{nameof(Structure)} {{Name: $name2}})
                CREATE (s1)-[r:{NeoStrings.IsPartOfRelation}]->(s2)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    name1 = childStructure.Name.Trim(),
                    name2 = parentStructure.Name.Trim()
                });
            });
        }
        #endregion

        #region Check relations
        public static async Task<bool> RelationExists(string label1, string property1Name, string property1Value,
                                                string label2, string property2Name, string property2Value,
                                                string relation)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{label1} {{ {property1Name}: $property1Value }})
                MATCH (n2:{label2} {{ {property2Name}: $property2Value }})
                RETURN EXISTS((n1)-[:{relation}]-(n2)) AS relationExists";

            property1Value = property1Value.Trim();
            property2Value = property2Value.Trim();
            var result = await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, 
                    new { property1Value, property2Value });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    return record["relationExists"].As<bool>();
                }

                return false;
            });

            return result;
        }

        #endregion

        #region Return related objects
        public static async Task<Structure> GetRelatedBaseFor(MilitaryPerson person)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(MilitaryPerson)} {{ FullName: $fullName }})
                MATCH (n2:{nameof(Structure)})
                WHERE ((n1)-[:{NeoStrings.registeredInRelation}]->(n2))
                RETURN n2.Name AS Name, n2.Type AS Type, n2.SpecialProperty AS SP";
            Structure milBase = new() { Name = Strings.noData };
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, 
                    new { fullName = person.FullName });

                var record = await resultCursor.SingleAsync();
                if (record != null)
                {
                    milBase = new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SP"].As<string>()
                    };
                }
            });

            return milBase;
        }
        public static async Task<ObservableCollection<Structure>> GetStructuresUnderControlFor(MilitaryPerson person)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(MilitaryPerson)} {{ FullName: $fullName }})
                MATCH (n2:{nameof(Structure)})
                WHERE ((n1)-[:{NeoStrings.commandsRelation}]->(n2))
                RETURN n2.Name AS Name, n2.Type AS Type, n2.SpecialProperty AS SP";
            ObservableCollection<Structure> structures = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { fullName = person.FullName });
                await resultCursor.ForEachAsync(record =>
                {
                    structures.Add(new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SP"].As<string>()
                    });                        
                });
            });

            return structures;
        }
        public static async Task<ObservableCollection<MilitaryPerson>> GetRelatedPersonsFor(Structure structure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Structure)} {{ Name: $name }})
                MATCH (n2:{nameof(MilitaryPerson)})
                WHERE ((n1)-[]-(n2))
                RETURN n2.FullName AS FullName, n2.Rank AS Rank,
                n2.Specialities AS Specialities, n2.SpecialProperty1 AS SP1, n2.SpecialProperty2 AS SP2";
            
            ObservableCollection<MilitaryPerson> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { name = structure.Name });
                await resultCursor.ForEachAsync(record =>
                {
                    objectsCollection.Add(new()
                    {
                        FullName = record["FullName"].As<string>(),
                        Rank = record["Rank"].As<string>(),
                        Specialities = record["Specialities"].As<string>(),
                        SpecialProperty1 = record["SP1"].As<string>(),
                        SpecialProperty2 = record["SP2"].As<string>(),
                    });
                });
            });

            return objectsCollection;
        }
        #endregion
    }
}
