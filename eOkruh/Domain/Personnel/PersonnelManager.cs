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
            await DatabaseSaver.SavePersonnelInfo(info);
        }
    }
}
