namespace eOkruh.Common.UserManagement
{
    internal static class RolesRepresentations
    {
        public static readonly Dictionary<UserRoles, string> roleStrings = new()
        {
            { UserRoles.Viewer, "Переглядач" },
            { UserRoles.Operator, "Оператор" },
            { UserRoles.Administrator, "Адміністратор" },
            { UserRoles.Owner, "Власник" }
        };
        
        public static readonly Dictionary<string, UserRoles> roles = new()
        {
            { "Переглядач", UserRoles.Viewer },
            { "Оператор", UserRoles.Operator },
            { "Адміністратор", UserRoles.Administrator },
            { "Власник", UserRoles.Owner }
        };
    }
}
