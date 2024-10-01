namespace eOkruh.Common
{
    internal static class Strings
    {
        public static readonly string noData = "-";
        public static readonly string secretData = "Приховано";
        public static readonly string separator = ", ";
        public static readonly string attention = "Увага!";
        public static readonly string cancel = "Скасувати";
        public static readonly string confirm = "Зроз, добре";
        public static readonly string edit = "Редагувати";
        public static readonly string save = "Зберегти";
        public static readonly string zero = "0";

        public static string[] SplitByComma(string input)
        {
            return input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string JoinWithComma(string[] input)
        {
            return string.Join(separator, input);
        }

        public static string CapitalizeFirstLetter(this string input)
        {
            return input[0].ToString().ToUpper() + input[1..];
        }
    }
}
