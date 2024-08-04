using eOkruh.Common.UserManagement;
using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseWriter
    {
        // TODO implement
        public static async Task SaveUser(this IDriver driver, User user)
        {
            using var session = driver.AsyncSession();
            var query = @"
                MERGE (u:User {Login: $login})
                ON CREATE SET u.FullName = $fullName, u.Password = $password, u.UserRole = $userRole
                ON MATCH SET u.FullName = $fullName, u.Password = $password, u.UserRole = $userRole";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new
                {
                    login = user.Login,
                    fullName = user.FullName,
                    password = user.Password,
                    userRole = user.UserRole
                });
            });
        }

    }
}
