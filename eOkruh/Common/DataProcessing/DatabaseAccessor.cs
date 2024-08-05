using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    internal static class DatabaseAccessor
    {
        public static readonly IDriver driver = GraphDatabase
            .Driver(Strings.databaseLocalConnectionString,
                AuthTokens.Basic(Strings.databaseUsername,
                Strings.databasePassword));

        public static async Task CloseDatabaseConnection()
        {
            await driver.DisposeAsync();
        }
    }
}
