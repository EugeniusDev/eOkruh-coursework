using eOkruh.Common;

namespace eOkruh.Domain.Personnel
{
    internal static class RankRepresentations
    {
        #region Ordinary
        public static readonly Dictionary<OrdinaryPersonnel, string> ordinaryStrings = new()
        {
            { OrdinaryPersonnel.Recruit, "Рекрут" },
            { OrdinaryPersonnel.Soldier, "Солдат" },
            { OrdinaryPersonnel.SeniorSoldier, "Старший солдат" }
        };
        public static readonly Dictionary<string, OrdinaryPersonnel> ordinary = new()
        {
            { "Рекрут", OrdinaryPersonnel.Recruit },
            { "Солдат", OrdinaryPersonnel.Soldier },
            { "Старший солдат", OrdinaryPersonnel.SeniorSoldier }
        };
        #endregion

        #region Sergeant
        public static readonly Dictionary<SergeantPersonnel, string> sergeantStrings = new()
        {        
            { SergeantPersonnel.JuniorSergeant, "Молодший сержант" },
            { SergeantPersonnel.Sergeant, "Сержант" },
            { SergeantPersonnel.SeniorSergeant, "Старший сержант" },
            { SergeantPersonnel.ChiefSergeant, "Головний сержант" },
            { SergeantPersonnel.StaffSergeant, "Штаб-сержант" },
            { SergeantPersonnel.MasterSergeant, "Майстер-сержант" },
            { SergeantPersonnel.SeniorMasterSergeant, "Старший майстер-сержант" },
            { SergeantPersonnel.ChiefMasterSergeant, "Головний майстер-сержант" },
        };
        public static readonly Dictionary<string, SergeantPersonnel> sergeant = new()
        {
            { "Молодший сержант", SergeantPersonnel.JuniorSergeant },
            { "Сержант", SergeantPersonnel.Sergeant },
            { "Старший сержант", SergeantPersonnel.SeniorSergeant },
            { "Головний сержант", SergeantPersonnel.ChiefSergeant },
            { "Штаб-сержант", SergeantPersonnel.StaffSergeant },
            { "Майстер-сержант", SergeantPersonnel.MasterSergeant },
            { "Старший майстер-сержант", SergeantPersonnel.SeniorMasterSergeant },
            { "Головний майстер-сержант", SergeantPersonnel.ChiefMasterSergeant },
        };
        #endregion

        #region Officer
        public static readonly Dictionary<OfficerPersonnel, string> officerStrings = new()
        {        
            { OfficerPersonnel.JuniorLieutenant, "Молодший лейтенант" },
            { OfficerPersonnel.Lieutenant, "Лейтенант" },
            { OfficerPersonnel.SeniorLieutenant, "Старший лейтенант" },
            { OfficerPersonnel.Captain, "Капітан" },

            { OfficerPersonnel.Major, "Майор" },
            { OfficerPersonnel.LieutenantColonel, "Підполковник" },
            { OfficerPersonnel.Colonel, "Полковник" },

            { OfficerPersonnel.BrigadeGeneral, "Бригадний генерал" },
            { OfficerPersonnel.GeneralMajor, "Генерал-майор" },
            { OfficerPersonnel.GeneralLieutenant, "Генерал-лейтенант" },
            { OfficerPersonnel.General, "Генерал" },
        };
        public static readonly Dictionary<string, OfficerPersonnel> officer = new()
        {
            { "Молодший лейтенант", OfficerPersonnel.JuniorLieutenant },
            { "Лейтенант", OfficerPersonnel.Lieutenant },
            { "Старший лейтенант", OfficerPersonnel.SeniorLieutenant },
            { "Капітан", OfficerPersonnel.Captain },

            { "Майор", OfficerPersonnel.Major },
            { "Підполковник", OfficerPersonnel.LieutenantColonel },
            { "Полковник", OfficerPersonnel.Colonel },

            { "Бригадний генерал", OfficerPersonnel.BrigadeGeneral },
            { "Генерал-майор", OfficerPersonnel.GeneralMajor },
            { "Генерал-лейтенант", OfficerPersonnel.GeneralLieutenant },
            { "Генерал", OfficerPersonnel.General },
        };
        #endregion
    }
}
