namespace eOkruh.Domain.Personnel
{
    public class MilitaryPerson
    {
        public string FullName { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Specialities { get; set; } = string.Empty;
        public string SpecialProperty1 { get; set; } = string.Empty;
        public string SpecialProperty2 { get; set; } = string.Empty;
        
        public bool IsOrdinary()
        {
            return RankRepresentations.ordinary.ContainsKey(Rank);
        }
        public bool IsSergeant()
        {
            return RankRepresentations.sergeant.ContainsKey(Rank);
        }
        public bool IsOfficer()
        {
            return RankRepresentations.officer.ContainsKey(Rank);
        }
    }
}
