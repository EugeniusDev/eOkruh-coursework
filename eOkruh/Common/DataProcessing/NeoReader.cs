using eOkruh.Common.UserManagement;
using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using Neo4j.Driver;
using System.Collections.ObjectModel;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoReader
    {
        #region UsersTab
        public static async Task<User?> GetUserByLogin(string login)
        {
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MATCH (u:{nameof(User)} {{Login: $login}})
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
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MATCH (u:{nameof(User)} {{Login: $login, Password: $password}})
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
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MATCH (u:{nameof(User)})
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
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = $@"
                MATCH (u:{nameof(User)} {{FullName: $userFullName}})
                OPTIONAL MATCH (u)-[r:{NeoStrings.assignedByRelation}]->(a:{nameof(User)})
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

            return result ?? throw new ArgumentException("Користувача не знайдено");
        }
        #endregion
        #region MilitaryPerson
        public static async Task<ObservableCollection<MilitaryPerson>> GetAllMilitaryPersons()
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n:{nameof(MilitaryPerson)})
                RETURN n.FullName AS FullName, n.Rank AS Rank,
                n.Specialities AS Specialities, n.SpecialProperty1 AS SP1, n.SpecialProperty2 AS SP2";

            ObservableCollection<MilitaryPerson> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query);

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
        #region Structures
        public static async Task<Structure> GetStructure(string name)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s:{nameof(Structure)} {{Name: $name}})
                OPTIONAL MATCH (s)-[:{NeoStrings.IsPartOfRelation}]->(ancS:{nameof(Structure)})
                RETURN s.Name AS Name, s.Type AS Type, s.SpecialProperty AS SpecialProperty";

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
                        SpecialProperty = record["SpecialProperty"].As<string>()
                    };

                    return structure;
                }

                return null;
            });

            return result ?? throw new ArgumentException("Вказаної структури не існує");
        }

        public static async Task<ObservableCollection<Structure>> GetAllStructures()
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s:{nameof(Structure)})
                RETURN s.Name AS Name, s.Type AS Type, s.SpecialProperty AS SP ";

            ObservableCollection<Structure> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query);

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

        public static async Task<ObservableCollection<Structure>> GetAllStructuresOfType(string structureType)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s:{nameof(Structure)} {{Type: $type}})
                RETURN s.Name AS Name, s.Type AS Type, s.SpecialProperty AS SP ";

            ObservableCollection<Structure> objectsCollection = [];
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { type = structureType });

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

        public static async Task<StructuresTab3PropsDto> GetBasesCountInfoFor(Structure structure)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (s1:{nameof(Structure)} {{Name: $name}})
                OPTIONAL MATCH (s1)<-[r:{NeoStrings.IsPartOfRelation}]-(s2:{nameof(Structure)})
                RETURN s1.Name AS Name, s1.Type AS Type, COUNT(r) AS BaseCount";

            var structureData = new StructuresTab3PropsDto();
            await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { name = structure.Name });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    structureData = new()
                    {
                        Prop1 = record["Name"].As<string>(),
                        Prop2 = record["Type"].As<string>(),
                        Prop3 = record["BaseCount"].As<int>().ToString()
                    };
                }
            });

            return structureData;
        }

        
        #endregion
    }
}
