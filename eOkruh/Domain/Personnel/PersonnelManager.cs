using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using System.Collections.ObjectModel;

namespace eOkruh.Domain.Personnel
{
    public static class PersonnelManager
    {
        public static readonly List<string> allRanks =
        [
            RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Recruit],
            RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Soldier],
            RankRepresentations.ordinaryStrings[OrdinaryPersonnel.SeniorSoldier],

            RankRepresentations.sergeantStrings[SergeantPersonnel.JuniorSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.Sergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.StaffSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.MasterSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorMasterSergeant],
            RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefMasterSergeant],

            RankRepresentations.officerStrings[OfficerPersonnel.JuniorLieutenant],
            RankRepresentations.officerStrings[OfficerPersonnel.Lieutenant],
            RankRepresentations.officerStrings[OfficerPersonnel.SeniorLieutenant],
            RankRepresentations.officerStrings[OfficerPersonnel.Captain],
            RankRepresentations.officerStrings[OfficerPersonnel.Major],
            RankRepresentations.officerStrings[OfficerPersonnel.LieutenantColonel],
            RankRepresentations.officerStrings[OfficerPersonnel.Colonel],
            RankRepresentations.officerStrings[OfficerPersonnel.BrigadeGeneral],
            RankRepresentations.officerStrings[OfficerPersonnel.GeneralMajor],
            RankRepresentations.officerStrings[OfficerPersonnel.GeneralLieutenant],
            RankRepresentations.officerStrings[OfficerPersonnel.General]
        ];
        public static readonly Dictionary<string, (string, string)> specialPropertyInfoTuples = new()
        {
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Recruit],
                ("Дата початку рекрутингу", "Місце рекрутингу (країна)") },
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Soldier],
                ("Кількість пройдених навчальних курсів", "Дата присвоєння звання") },
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.SeniorSoldier],
                ("Досвід участі у бойових діях (у місяцях)", "Дата підвищення в званні") },

            { RankRepresentations.sergeantStrings[SergeantPersonnel.JuniorSergeant],
                ("Кількість підлеглих", "Дата призначення на керівну посаду") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.Sergeant],
                ("Рівень бойової підготовки (за шкалою від 1 до 10)", "Кількість виконаних наказів") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorSergeant],
                ("Стаж у керівних посадах (у роках)", "Кількість нагород за службу") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefSergeant],
                ("Рівень військової кваліфікації (за шкалою від 1 до 10)", "Кількість виконаних бойових завдань") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.StaffSergeant],
                ("Кількість проведених операцій", "Рівень авторитету серед підлеглих (за шкалою від 1 до 10)") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.MasterSergeant],
                ("Тривалість служби в армії (у роках)", "Кількість проведених тренувань для підлеглих") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorMasterSergeant],
                ("Кількість проведених стратегічних операцій", "Співвідношення успішних тактичних операцій до провальних") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefMasterSergeant],
                ("Кількість успішних операцій під керівництвом", "Рівень впливу на оперативне планування") },

            { RankRepresentations.officerStrings[OfficerPersonnel.JuniorLieutenant],
                ("Рік випуску з військової академії", "Середній бал під час навчання") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Lieutenant],
                ("Кількість підлеглих під час першої служби", "Кількість виконаних бойових завдань") },
            { RankRepresentations.officerStrings[OfficerPersonnel.SeniorLieutenant],
                ("Стаж у лейтенантському званні (у роках)", "Кількість командних рішень під час операцій") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Captain],
                ("Кількість успішних операцій під командуванням", "Кількість нагород за військову службу") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Major],
                ("Кількість підлеглих під час командування", "Тривалість служби у званні майора") },
            { RankRepresentations.officerStrings[OfficerPersonnel.LieutenantColonel],
                ("Кількість успішних стратегічних операцій", "Кількість років на керівних посадах") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Colonel],
                ("Кількість проведених військових навчань", "Кількість нагород за досягнення в командуванні") },
            { RankRepresentations.officerStrings[OfficerPersonnel.BrigadeGeneral],
                ("Рівень стратегічного мислення (за шкалою від 1 до 10)", "Кількість реалізованих стратегічних планів") },
            { RankRepresentations.officerStrings[OfficerPersonnel.GeneralMajor],
                ("Кількість проведених операцій на високому рівні", "Рівень впливу на прийняття стратегічних рішень") },
            { RankRepresentations.officerStrings[OfficerPersonnel.GeneralLieutenant],
                ("Кількість років на генералітеті", "Рівень участі у міжнародних операціях (за шкалою від 1 до 10)") },
            { RankRepresentations.officerStrings[OfficerPersonnel.General],
                ("Кількість років служби у званні генерала", "Загальне ставлення особового складу до методів оборони генерала (за шкалою від 1 до 10)") }
        };

        public static bool IsMilitaryPersonInfoValid(MilitaryPerson person, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(person.FullName)
                || person.FullName.Equals(Strings.noData))
            {
                errorMessage = "Поле з повним ім'ям є обов'язковим до заповнення";
                return false;
            }
            if (string.IsNullOrWhiteSpace(person.Rank)
                || person.Rank.Equals(Strings.noData))
            {
                errorMessage = "Будь ласка, оберіть коректне звання";
                return false;
            }
            if (string.IsNullOrWhiteSpace(person.SpecialProperty1)
                || person.SpecialProperty1.Equals(Strings.noData))
            {
                errorMessage = "Будь ласка, введіть дані для особливої властивості 1";
                return false;
            }
            if (string.IsNullOrWhiteSpace(person.SpecialProperty2)
                || person.SpecialProperty2.Equals(Strings.noData))
            {
                errorMessage = "Будь ласка, введіть дані для особливої властивості 2";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosByRank(string rank)
        {
            return await DatabaseReader.GetPersonnelInfosByRank(rank, Strings.noData);
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosByRankWithScope(string rank, string structureName)
        {
            return await DatabaseReader.GetPersonnelInfosByRank(rank, structureName);
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosBySpecialityAndStructure(string speciality, string structureName)
        {
            return await DatabaseReader.GetPersonnelInfosBySpeciality(speciality, structureName);
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetAllInfos()
        {
            return await DatabaseReader.GetAllPersonnelInfos();
        }

        public static async Task SavePersonnelInfo(FullPersonnelInfo info)
        {
            await DatabaseDeleter.DeletePersonRelations(info.MilitaryPerson);
            await DatabaseSaver.SavePersonnelInfo(info);
        }
    }
}
