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


        // TODO implement
        // kind of adapter between database operations
        // and getting required info for them from objects


        public static Task<ObservableCollection<FullPersonnelInfo>> GetAllInfos()
        {
            throw new NotImplementedException();
        }

        public static Task<ObservableCollection<FullPersonnelInfo>> GetInfosByRank(string rank)
        {
            throw new NotImplementedException();
        }

        public static Task<ObservableCollection<FullPersonnelInfo>> GetInfosByRankWithScope(string rank, string structureName)
        {
            throw new NotImplementedException();
        }

        public static Task<ObservableCollection<FullPersonnelInfo>> GetInfosBySpecialityAndStructure(string speciality, string structureName)
        {
            throw new NotImplementedException();
        }

        static void test()
        {
            // TODO use that structure to split and validate stuff
            string test = string.Empty;
            var testtest = test.Split(Strings.separator,
                StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
