using eOkruh.Common.UserManagement;
using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseReader
    {
        public static async Task<User?> GetUserByLogin(string login)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(Strings.userDatabase));
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
                o.WithDatabase(Strings.userDatabase));
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
                o.WithDatabase(Strings.userDatabase));
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
                o.WithDatabase(Strings.userDatabase));
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
    }
}
