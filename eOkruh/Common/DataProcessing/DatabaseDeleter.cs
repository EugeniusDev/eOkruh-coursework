using eOkruh.Common.UserManagement;

namespace eOkruh.Common.DataProcessing
{
    public static class DatabaseDeleter
    {
        public static async Task DeleteUser(User user)
        {
            using var session = DatabaseAccessor.driver.AsyncSession(o =>
                o.WithDatabase(Strings.userDatabase));
            var query = @"
                MATCH (u:User {FullName: $fullName})
                OPTIONAL MATCH (u)-[r:ASSIGNED_BY]->()
                DELETE r, u";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { fullName = user.FullName });
            });
        }
    }
}
