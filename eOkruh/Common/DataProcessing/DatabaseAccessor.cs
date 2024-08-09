using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    internal static class DatabaseAccessor
    {
        public static readonly IDriver driver = GraphDatabase
            .Driver(DatabaseStrings.databaseLocalConnectionString,
                AuthTokens.Basic(DatabaseStrings.databaseUsername,
                DatabaseStrings.databasePassword));

        public static async Task CloseDatabaseConnection()
        {
            await driver.DisposeAsync();
        }
    }
}
