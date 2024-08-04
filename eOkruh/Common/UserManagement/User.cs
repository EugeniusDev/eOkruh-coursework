namespace eOkruh.Common.UserManagement
{
    public class User
    {
        public string FullName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserRole { get; set; } = RolesRepresentations
            .roleStrings[UserRoles.Viewer];
    }
}
