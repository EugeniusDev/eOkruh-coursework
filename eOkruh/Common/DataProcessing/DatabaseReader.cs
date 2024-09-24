using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using Neo4j.Driver;
using System.Collections.ObjectModel;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseReader
    {
        #region UsersTab
        public static async Task<User?> GetUserByLogin(string login)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MATCH (u:User {Login: $login})
                RETURN u.FullName as FullName, u.Login as Login, u.Password as Password, u.UserRole as UserRole";

            var result = await session.ExecuteReadAsync(async session =>
            {
                var resultCursor = await session.RunAsync(query, new { login });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    return new User
                    {
                        FullName = record["FullName"].As<string>(),
                        Login = record["Login"].As<string>(),
                        Password = record["Password"].As<string>(),
                        UserRole = record["UserRole"].As<string>()
                    };
                }

                return null;
            });

            return result;
        }

        public static async Task<User?> GetUserByLoginAndPassword(string login, string password)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MATCH (u:User {Login: $login, Password: $password})
                RETURN u.FullName as FullName, u.Login as Login, u.Password as Password, u.UserRole as UserRole";

            var result = await session.ExecuteReadAsync(async session =>
            {
                var resultCursor = await session.RunAsync(query, new { login, password });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    return new User
                    {
                        FullName = record["FullName"].As<string>(),
                        Login = record["Login"].As<string>(),
                        Password = record["Password"].As<string>(),
                        UserRole = record["UserRole"].As<string>()
                    };
                }

                return null;
            });

            return result;
        }

        public static async Task<List<string>> GetAllUserFullNames()
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MATCH (u:User)
                RETURN u.FullName AS FullName";

            var fullNames = await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query);
                List<string> fullNamesList = [];

                await resultCursor.ForEachAsync(record =>
                {
                    fullNamesList.Add(record["FullName"].As<string>());
                });

                return fullNamesList;
            });

            return fullNames;
        }

        public static async Task<FullUserInfo> GetFullUserInfo(string userFullName)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(DatabaseStrings.userDatabase));
            var query = @"
                MATCH (u:User {FullName: $userFullName})
                OPTIONAL MATCH (u)-[r:ASSIGNED_BY]->(a:User)
                RETURN u.FullName AS FullName, u.Login AS Login, u.Password AS Password,
                       u.UserRole AS UserRole, u.DateOfLogin AS DateOfLogin,
                       a.FullName AS AssigneeFullName, r.assignedDate AS AssigningDate";

            var result = await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { userFullName });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    var user = new User
                    {
                        FullName = record["FullName"].As<string>(),
                        Login = record["Login"].As<string>(),
                        Password = record["Password"].As<string>(),
                        UserRole = record["UserRole"].As<string>(),
                        DateOfLogin = record["DateOfLogin"].As<string>()
                    };

                    return new FullUserInfo
                    {
                        User = user,
                        AssigneeFullName = record["AssigneeFullName"]?.As<string>() ?? Strings.noData,
                        AssigningDate = record["AssigningDate"]?.As<string>() ?? Strings.noData
                    };
                }

                return null;
            });

            return result ?? throw new ArgumentException("User was not found");
        }
        #endregion

        #region PersonnelTab
        public static async Task<ObservableCollection<FullPersonnelInfo>> GetPersonnelInfosByRank(string rank, string structureScope)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var result = await session.ExecuteReadAsync(async tx =>
            {
                var query = @"
                    MATCH (p:MilitaryPerson {Rank: $rank})-[:COMMANDS]->(s:Structure)
                    WHERE s.Name CONTAINS $structureScope
                    OPTIONAL MATCH (p)-[:REGISTERED_IN]->(mb:Structure)
                    RETURN p, collect(DISTINCT s.Name) AS structures, mb.Name AS militaryBase
                ";
                var cursor = await tx.RunAsync(query, new { rank, structureScope });
                var personnelInfos = new ObservableCollection<FullPersonnelInfo>();

                await cursor.ForEachAsync(record =>
                {
                    var person = record["p"].As<INode>().Properties;
                    var structures = record["structures"].As<string[]>();
                    var militaryBase = record["militaryBase"].As<string>();

                    personnelInfos.Add(new FullPersonnelInfo
                    {
                        MilitaryPerson = new MilitaryPerson
                        {
                            FullName = person["FullName"].As<string>(),
                            Rank = person["Rank"].As<string>(),
                            Specialities = person["Specialities"].As<string>(),
                            SpecialProperty1 = person["SpecialProperty1"].As<string>(),
                            SpecialProperty2 = person["SpecialProperty2"].As<string>()
                        },
                        MilitaryBase = militaryBase,
                        StructuresUnderControl = string.Join(", ", structures)
                    });
                });

                return personnelInfos;
            });

            return result;
        }

    public static async Task<ObservableCollection<FullPersonnelInfo>> GetPersonnelInfosBySpeciality(string rank, string speciality)
    {
        using var session = DatabaseAccessor.driver.AsyncSession();
        var result = await session.ExecuteReadAsync(async tx =>
        {
            var query = @"
                MATCH (p:MilitaryPerson {Rank: $rank})
                WHERE p.Specialities CONTAINS $speciality
                OPTIONAL MATCH (p)-[:COMMANDS]->(s:Structure)
                OPTIONAL MATCH (p)-[:REGISTERED_IN]->(mb:Structure)
                RETURN p, collect(DISTINCT s.Name) AS structures, mb.Name AS militaryBase
            ";
            var cursor = await tx.RunAsync(query, new { rank, speciality });
            var personnelInfos = new ObservableCollection<FullPersonnelInfo>();

            await cursor.ForEachAsync(record =>
            {
                var person = record["p"].As<INode>().Properties;
                var structures = record["structures"].As<string[]>();
                var militaryBase = record["militaryBase"].As<string>();

                personnelInfos.Add(new FullPersonnelInfo
                {
                    MilitaryPerson = new MilitaryPerson
                    {
                        FullName = person["FullName"].As<string>(),
                        Rank = person["Rank"].As<string>(),
                        Specialities = person["Specialities"].As<string>(),
                        SpecialProperty1 = person["SpecialProperty1"].As<string>(),
                        SpecialProperty2 = person["SpecialProperty2"].As<string>()
                    },
                    MilitaryBase = militaryBase,
                    StructuresUnderControl = string.Join(", ", structures)
                });
            });

            return personnelInfos;
        });

        return result;
    }

    public static async Task<ObservableCollection<FullPersonnelInfo>> GetAllPersonnelInfos()
    {
        using var session = DatabaseAccessor.driver.AsyncSession();
        var result = await session.ExecuteReadAsync(async tx =>
        {
            var query = @"
                MATCH (p:MilitaryPerson)
                OPTIONAL MATCH (p)-[:COMMANDS]->(s:Structure)
                OPTIONAL MATCH (p)-[:REGISTERED_IN]->(mb:Structure)
                RETURN p, collect(DISTINCT s.Name) AS structures, mb.Name AS militaryBase
            ";
            var cursor = await tx.RunAsync(query);
            var personnelInfos = new ObservableCollection<FullPersonnelInfo>();

            await cursor.ForEachAsync(record =>
            {
                var person = record["p"].As<INode>().Properties;
                var structures = record["structures"].As<string[]>();
                var militaryBase = record["militaryBase"].As<string>();

                personnelInfos.Add(new FullPersonnelInfo
                {
                    MilitaryPerson = new MilitaryPerson
                    {
                        FullName = person["FullName"].As<string>(),
                        Rank = person["Rank"].As<string>(),
                        Specialities = person["Specialities"].As<string>(),
                        SpecialProperty1 = person["SpecialProperty1"].As<string>(),
                        SpecialProperty2 = person["SpecialProperty2"].As<string>()
                    },
                    MilitaryBase = militaryBase,
                    StructuresUnderControl = string.Join(", ", structures)
                });
            });

            return personnelInfos;
        });

        return result;
    }
    #endregion

    #region Structures
    public static async Task<Structure> GetStructure(string name)
        {
            using var session = DatabaseAccessor.driver.AsyncSession();
            var query = @"
                MATCH (s:Structure {Name: $name})
                OPTIONAL MATCH (s)-[:IS_PART_OF]->(ancS:Structure)
                RETURN s.Name AS Name, s.Type AS Type, s.SpecialProperty AS SpecialProperty,
                COALESCE(ancS.Name, $noData) AS AncestorStructureName";

            var result = await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { name, Strings.noData });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    var structure = new Structure
                    {
                        Name = record["Name"].As<string>(),
                        Type = record["Type"].As<string>(),
                        SpecialProperty = record["SpecialProperty"].As<string>(),
                        AncestorStructureName = record["AncestorStructureName"].As<string>()
                    };

                    return structure;
                }

                return null;
            });

            return result ?? throw new ArgumentException("Structure was not found");
        }
        #endregion
    }
}
