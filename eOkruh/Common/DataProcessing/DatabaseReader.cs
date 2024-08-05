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
    }
}
