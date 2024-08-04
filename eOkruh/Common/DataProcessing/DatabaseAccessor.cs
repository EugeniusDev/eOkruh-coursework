using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    internal static class DatabaseAccessor
    {
        // TODO maybe structure like this to prevent wrapping everything with open/close connection
        static async Task Execute(Action action)
        {
            var driver = OpenConnection();
            action.Invoke();
            await CloseConnection(driver);
        }

        public static IDriver OpenConnection()
        {
            return GraphDatabase.Driver(Strings.databaseLocalConnectionString, 
                AuthTokens.Basic(Strings.databaseUsername,
                Strings.databasePassword));
        }

        public static async Task CloseConnection(IDriver driver)
        {
            await driver.DisposeAsync();
        }
    }
}
