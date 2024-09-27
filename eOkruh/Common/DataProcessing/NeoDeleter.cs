using eOkruh.Common.UserManagement;
using eOkruh.Domain.Personnel;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoDeleter
    {
        public static async Task DeleteUser(User user)
        {
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = @"
                MATCH (u:User {FullName: $fullName})
                OPTIONAL MATCH (u)-[r:ASSIGNED_BY]->()
                DELETE r, u";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { fullName = user.FullName.Trim() });
            });
        }

        public static async Task DeleteMilitaryPerson(MilitaryPerson person)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = @"
                MATCH (p:Person {FullName: $fullName})
                DETACH DELETE p";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query, new { fullName = person.FullName.Trim() });
            });
        }

        #region Global
        public static async Task DeleteMainDatabase()
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = @"
                MATCH (n)
                DETACH DELETE n";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query);
            });
        }
        public static async Task DeleteUserDatabase()
        {
            using var session = NeoAccessor.driver.AsyncSession(o =>
                o.WithDatabase(NeoStrings.userDatabase));
            var query = @"
                MATCH (n)
                DETACH DELETE n";

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(query);
            });
        }
        #endregion
    }
}
