using eOkruh.Common.UserManagement;
using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseReader
    {
        public static async Task<User?> GetUserByLoginAndPassword(this IDriver driver, string login, string password)
        {
            using var session = driver.AsyncSession();
            var query = @"
                MATCH (u:User {Login: $login, Password: $password})
                RETURN u.FullName as FullName, u.Login as Login, u.Password as Password, u.UserRole as UserRole";

            var result = await session.ExecuteReadAsync(async session =>
            {
                var resultCursor = await session.RunAsync(query, new { login, password });
                // TODO get single user
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

        public static User? RetrieveUser(string login, string password)
        {
            // TODO implement. If such user does not exists, return null
            return new() { FullName="Amogus" };

            return null;
        }
    }
}
