using eOkruh.Common.UserManagement;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseSaver
    {
        public static async Task SaveUser(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
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

        public static async Task SetNewPassword(string userLogin, string newPassword)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o => 
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MATCH (u:User {Login: $userLogin})
                SET u.Password = $newPassword
                RETURN u";
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { userLogin, newPassword });
            });
        }        
    }
}
