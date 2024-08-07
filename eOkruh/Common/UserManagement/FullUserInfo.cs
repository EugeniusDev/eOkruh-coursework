using eOkruh.Common.DataProcessing;

namespace eOkruh.Common.UserManagement
{
    public class FullUserInfo
    {
        public User User { get; set; } = new();
        public string AssigneeFullName { get; set; } = Strings.noData;
        public string AssigningDate { get; set; } = Strings.noData;

        public override string ToString()
        {
            string output = User.ToString();
            output += AssigneeFullName.Equals(Strings.noData)
                ? " Немає даних про того, хто призначив цього користувача."
                : $" Призначено користувачем з ПІБ: {AssigneeFullName}, " +
                $"дата призначення: {AssigningDate}";
            return output;
        }
    }
}
