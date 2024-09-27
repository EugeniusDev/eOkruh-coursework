using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    internal static class NeoAccessor
    {
        public static readonly IDriver driver = GraphDatabase
            .Driver(NeoStrings.databaseLocalConnectionString,
                AuthTokens.Basic(NeoStrings.databaseUsername,
                NeoStrings.databasePassword));

        public static async Task CloseDatabaseConnection()
        {
            await driver.DisposeAsync();
        }
    }
}
