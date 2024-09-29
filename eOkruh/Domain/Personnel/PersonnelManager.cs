using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.MilitaryStructures;
using System.Collections.ObjectModel;
using System.Dynamic;

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
                || person.Rank.Equals(Strings.noData)
                || !RankExists(person.Rank))
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
        private static bool RankExists(string rank)
        {
            if (rank.Length < 2)
            {
                return false;
            }
            string correctRank = rank[0].ToString().ToUpper() + rank[1..];
            return RankRepresentations.ordinaryStrings.ContainsValue(correctRank)
                || RankRepresentations.sergeantStrings.ContainsValue(correctRank)
                || RankRepresentations.officerStrings.ContainsValue(correctRank);
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetAllInfos()
        {
            var militaryPersons = await NeoReader.GetAllMilitaryPersons();
            ObservableCollection<FullPersonnelInfo> allInfos = [];
            foreach (MilitaryPerson person in militaryPersons)
            {
                allInfos.Add(await FormFullPersonnelInfo(person));
            }
            return allInfos;
        }
        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosByRank(string rank)
        {
            var allInfos = await GetAllInfos();
            return GetInfosByRankFrom(rank, allInfos);
        }
        private static ObservableCollection<FullPersonnelInfo> GetInfosByRankFrom(string rank, ObservableCollection<FullPersonnelInfo> infos)
        {
            var filteredInfos = infos.Where(info => info.MilitaryPerson.Rank.ToLower()
                    .Equals(rank.ToLower()));
            return [.. filteredInfos];
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetScopedInfosWithRank(string rank, string structureName)
        {
            return GetInfosByRankFrom(rank,
                await GetInfosWithScope(structureName));
        }
        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosWithScope(string structureName)
        {
            var allInfos = await GetAllInfos();
            var filteredInfos = allInfos
                .Where(info => info.StructuresUnderControl
                    .Contains(structureName, StringComparison.CurrentCultureIgnoreCase));
            ObservableCollection<FullPersonnelInfo> scopedInfos = [.. filteredInfos];
            foreach (var childScopedInfo in await GetInfosFromChildStructures(structureName))
            {
                scopedInfos.Add(childScopedInfo);
            }
            return scopedInfos;
        }
        public static async Task<ObservableCollection<FullPersonnelInfo>> GetInfosFromChildStructures(string structureName)
        {
            var childStructuresNames = await GetChildStructureNamesOf(structureName);
            ObservableCollection<FullPersonnelInfo> infos = [];
            foreach (var childStructureName in childStructuresNames)
            {
                var relatedPersons = await NeoRelationManager
                    .GetRelatedPersonsFor(new() { Name = childStructureName });
                foreach (var person in relatedPersons)
                {
                    infos.Add(await FormFullPersonnelInfo(person));
                }
            }
            return infos;
        }

        public static async Task<List<string>> GetChildStructureNamesOf(string structureName)
        {
            var childStructures = await StructureManager.GetAllChildStructures(
                new() { Name = structureName });
            return [.. childStructures.ToNames()];
        }

        public static async Task<ObservableCollection<FullPersonnelInfo>> GetScopedInfosWithSpeciality(string speciality, string structureName)
        {
            return GetInfosBySpecialityFrom(speciality,
                await GetInfosWithScope(structureName));
        }
        private static ObservableCollection<FullPersonnelInfo> GetInfosBySpecialityFrom(string speciality,
            ObservableCollection<FullPersonnelInfo> infos)
        {
            var filteredInfos = infos
                .Where(info => info.MilitaryPerson.Specialities
                    .Contains(speciality, StringComparison.CurrentCultureIgnoreCase));
            return [.. filteredInfos];
        }

        public static async Task<FullPersonnelInfo> FormFullPersonnelInfo(MilitaryPerson person)
        {
            var relatedBase = await NeoRelationManager.GetRelatedBaseFor(person);
            string milBaseName = relatedBase.Name;
            var structuresUC = await NeoRelationManager.GetStructuresUnderControlFor(person);
            string structuresUnderControlNames = structuresUC.Count == 0
                ? Strings.noData : Strings.JoinWithComma(structuresUC.ToNames());
            return new()
            {
                MilitaryPerson = person,
                MilitaryBase = milBaseName,
                StructuresUnderControl = structuresUnderControlNames
            };
        }

        public static string[] ToNames(this ObservableCollection<Structure> structures)
        {
            List<string> names = [];
            foreach (var structure in structures)
            {
                names.Add(structure.Name);
            }
            return [.. names];
        }

        public static async Task SavePersonnelInfo(FullPersonnelInfo info)
        {
            if (IsEmptyOrDefault(info.MilitaryBase))
            {
                throw new ArgumentException("Не вказано військову частину, до якої " +
                    "приписаний військовослужбовець");
            }
            if (!await RelatedStructuresExist(info))
            {
                throw new ArgumentException("Вказано неіснуючі структури");
            }

            if (await MilitaryPersonNodeExists(info.MilitaryPerson))
            {
                await NeoDeleter.DeleteMilitaryPerson(info.MilitaryPerson);
            }
            await NeoSaver.SavePerson(info.MilitaryPerson);
            await CreateRelationsToStructures(info);
        }

        private static bool IsEmptyOrDefault(string dataString)
        {
            return string.IsNullOrWhiteSpace(dataString)
                || dataString.Equals(Strings.noData);
        }
        private static async Task<bool> MilitaryPersonNodeExists(MilitaryPerson person)
        {
            return await NeoValidator.NodeExists(nameof(MilitaryPerson),
                "FullName", person.FullName);
        }

        private static async Task<bool> RelatedStructuresExist(FullPersonnelInfo info)
        {
            if (!await StructureManager
                .StructureExists(new() { Name = info.MilitaryBase}))
            {
                return false;
            }
            foreach (string structureName in Strings
                .SplitByComma(info.StructuresUnderControl))
            {
                if (!await StructureManager
                    .StructureExists(new() { Name = structureName }))
                {
                    return false;
                }
            }

            return true;
        }

        private static async Task CreateRelationsToStructures(FullPersonnelInfo info)
        {
            await NeoRelationManager.MakeRegisteredIn(info.MilitaryPerson, 
                new() { Name = info.MilitaryBase });
            string[] structuresUnderControlNames = Strings
                .SplitByComma(info.StructuresUnderControl);
            foreach (string structureName in structuresUnderControlNames)
            {
                await NeoRelationManager.MakeCommands(info.MilitaryPerson,
                    new() { Name = structureName });
            }
        }
    }
}
