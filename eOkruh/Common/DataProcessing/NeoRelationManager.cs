﻿using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using eOkruh.Domain.Property;
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
                CREATE (s1)-[r:{NeoStrings.isPartOfRelation}]->(s2)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    name1 = childStructure.Name.Trim(),
                    name2 = parentStructure.Name.Trim()
                });
            });
        }

        public static async Task MakeHasProperty(Structure structure, Property property)
        {
            string propertyNodeName;
            if (property is Weapon)
            {
                propertyNodeName = nameof(Weapon);
            }
            else
            {
                propertyNodeName = nameof(Equipment);
            }

            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s:{nameof(Structure)} {{Name: $structureName}}), (p:{propertyNodeName} {{Name: $propertyName}})
                CREATE (s)-[r:{NeoStrings.hasRelation}]->(p)";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    structureName = structure.Name.Trim(),
                    propertyName = property.Name.Trim()
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

        public static async Task<ObservableCollection<Structure>> GetAllChildStructures(Structure parentStructure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Structure)} {{Name: $parentStructureName}})<-[:{NeoStrings.isPartOfRelation}*]-(n2:{nameof(Structure)})
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

        public static async Task<MilitaryPerson> GetBaseCommander(Structure milBase)
        {
            if (!milBase.IsBase())
            {
                throw new ArgumentException("Вкажіть структуру, яка є військовою частиною");
            }
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Structure)} {{ Name: $name }})
                MATCH (n2:{nameof(MilitaryPerson)})
                WHERE ((n1)<-[:{NeoStrings.commandsRelation}]-(n2))
                RETURN n2.FullName AS commanderFullName, n2.Rank AS Rank,
                n2.Specialities AS Specialities, n2.SpecialProperty1 AS SP1, n2.SpecialProperty2 AS SP2";
            MilitaryPerson baseCommander = new();
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { name = milBase.Name });

                var record = await resultCursor.SingleAsync();
                if (record != null)
                {
                    baseCommander = new()
                    {
                        FullName = record["commanderFullName"].As<string>(),
                        Rank = record["Rank"].As<string>(),
                        Specialities = record["Specialities"].As<string>(),
                        SpecialProperty1 = record["SP1"].As<string>(),
                        SpecialProperty2 = record["SP2"].As<string>(),
                    };
                }
            });

            return baseCommander;
        }

        public static async Task<Structure> GetAncestoryStructureFor(Structure structure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Structure)} {{ Name: $name }})
                MATCH (n2:{nameof(Structure)})
                WHERE ((n1)-[:{NeoStrings.isPartOfRelation}]->(n2))
                RETURN n2.Name AS Name, n2.Type AS Type, n2.SpecialProperty AS SP";
            Structure ancestoryStructure = new();
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { name = structure.Name });

                var record = await resultCursor.SingleAsync();
                if (record != null)
                {
                    ancestoryStructure = new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SP"].As<string>()
                    };
                }
            });

            return ancestoryStructure;
        }

        public static async Task<ObservableCollection<Structure>> GetMasterStructuresFor(Property property)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            string propertyNodeName = property is Weapon ? nameof(Weapon) : nameof(Equipment);
            var query = $@"
                MATCH (n1:{propertyNodeName} {{ Name: $name }})
                MATCH (n2:{nameof(Structure)})
                WHERE ((n1)<-[:{NeoStrings.hasRelation}]-(n2))
                RETURN n2.Name AS Name, n2.Type AS Type, n2.SpecialProperty AS SP";

            ObservableCollection<Structure> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { name = property.Name });
                await resultCursor.ForEachAsync(record =>
                {
                    objectsCollection.Add(new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SP"].As<string>()
                    });
                });
            });

            return objectsCollection;
        }

        public static async Task<ObservableCollection<Weapon>> GetStructureOwnedWeaponsOfType(Structure structure, string wpType)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Weapon)} {{Type: $type}})
                MATCH (n2:{nameof(Structure)} {{ Name: $name }})
                WHERE ((n1)<-[:{NeoStrings.hasRelation}]-(n2))
                RETURN n1.Name AS Name, n1.Type AS Type, n1.SpecialProperty1 AS SP1, n1.SpecialProperty2 AS SP2";

            ObservableCollection<Weapon> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { type = wpType, name = structure.Name });
                await resultCursor.ForEachAsync(record =>
                {
                    objectsCollection.Add(new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty1 = record["SP1"].As<string>(),
                        SpecialProperty2 = record["SP2"].As<string>()
                    });
                });
            });

            return objectsCollection;
        }
        public static async Task<ObservableCollection<Equipment>> GetStructureOwnedEquipmentOfType(Structure structure, string eqType)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n1:{nameof(Equipment)} {{Type: $type}})
                MATCH (n2:{nameof(Structure)} {{ Name: $name }})
                WHERE ((n1)<-[:{NeoStrings.hasRelation}]-(n2))
                RETURN n1.Name AS Name, n1.Type AS Type, n1.SpecialProperty1 AS SP1, n1.SpecialProperty2 AS SP2";

            ObservableCollection<Equipment> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { type = eqType, name = structure.Name });
                await resultCursor.ForEachAsync(record =>
                {
                    objectsCollection.Add(new()
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty1 = record["SP1"].As<string>(),
                        SpecialProperty2 = record["SP2"].As<string>()
                    });
                });
            });

            return objectsCollection;
        }

        public static async Task<List<(string, string, int)>> GetTuplesWithBasesAndOwnedPropertyCount(string propertyName)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            string parentStructureType = StructureTypeStringPairs.typeStrings[StructureTypes.Base];
            var query = $@"
                MATCH (property:{propertyName})<-[:{NeoStrings.hasRelation}]-(structure:{nameof(Structure)})-[:{NeoStrings.isPartOfRelation}*]->(parentStructure:{nameof(Structure)} {{Type: $parentType}})
                WITH parentStructure, property.Type AS propertyType, COUNT(property) AS propertyCount
                RETURN parentStructure.Name AS containingBaseName, propertyType, SUM(propertyCount) AS totalPropertyCount";

            List<(string, string, int)> tuplesCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { parentType = parentStructureType });
                await resultCursor.ForEachAsync(record =>
                {
                    tuplesCollection.Add((
                        record["containingBaseName"].As<string>(),
                        record["propertyType"].As<string>(),
                        record["totalPropertyCount"].As<int>()
                        ));
                });
            });

            return tuplesCollection;
        }

        public static async Task<IEnumerable<string>> GetBasesContainingPropertyOfType(string propertyName, string propertyType)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            string parentStructureType = StructureTypeStringPairs.typeStrings[StructureTypes.Base];
            var query = $@"
                MATCH (w:{propertyName} {{Type: $type}})<-[:HAS]-(structure:Structure)-[:IS_PART_OF*]->(parentBase:Structure {{Type: $parentType}})
                RETURN parentBase.Name AS baseName";

            List<string> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query,
                    new { type = propertyType, parentType = parentStructureType });
                await resultCursor.ForEachAsync(record =>
                {
                    objectsCollection.Add(record["baseName"].As<string>());
                });
            });

            return objectsCollection.Distinct();
        }
        #endregion
    }
}
