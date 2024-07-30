namespace eOkruh.Common.UserManagement
{
    class User
    {
        public string FullName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserTypes UserType { get; set; } = UserTypes.Viewer;
    }
}
