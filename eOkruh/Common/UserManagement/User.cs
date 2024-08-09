namespace eOkruh.Common.UserManagement
{
    public class User
    {
        public string FullName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserRole { get; set; } = RolesRepresentations
            .roleStrings[UserRoles.Viewer];
        public string DateOfLogin { get; set; } = Strings.noData;

        public void SetCurrentDateOfLogin()
        {
            DateOfLogin = DateTime.Now.ToString();
        }

        public bool IsViewer()
        {
            return UserRole.Equals(RolesRepresentations
                .roleStrings[UserRoles.Viewer]);
        }

        public bool IsOperator()
        {
            return UserRole.Equals(RolesRepresentations
                .roleStrings[UserRoles.Operator]);
        }

        public bool IsAdministrator()
        {
            return UserRole.Equals(RolesRepresentations
                .roleStrings[UserRoles.Administrator]);
        }

        public bool IsOwner()
        {
            return UserRole.Equals(RolesRepresentations
                .roleStrings[UserRoles.Owner]);
        }

        public override string ToString()
        {
            string output = $"ПІБ: {FullName}, логін: {Login}, " +
                $"пароль: {Password}, роль: {UserRole}";
            output += DateOfLogin is null || DateOfLogin.Equals(Strings.noData)
                ? ". Немає даних про дату крайнього входу."
                : $", дата крайнього входу: {DateOfLogin}.";

            return output;
        }
    }
}
