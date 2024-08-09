namespace eOkruh.Common.DataProcessing
{
    internal static class Strings
    {
        public static readonly string noData = "-";
        public static readonly string separator = ", ";
        public static readonly string attention = "Увага!";
        public static readonly string cancel = "Скасувати";
        public static readonly string confirm = "Зроз, добре";
        public static readonly string edit = "Редагувати";
        public static readonly string save = "Зберегти";
        #region Database connection
        public static readonly string databaseLocalConnectionString = "bolt://localhost:7687";
        public static readonly string databaseUsername = "neo4j";
        public static readonly string databasePassword = "testPassword";
        public static readonly string userDatabase = "users";
        #endregion
    }
}
