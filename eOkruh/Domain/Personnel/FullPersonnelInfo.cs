namespace eOkruh.Domain.Personnel
{
    public class FullPersonnelInfo
    {
        public MilitaryPerson MilitaryPerson { get; set; } = new();
        public string MilitaryBase { get; set; } = string.Empty;
        public string StructuresUnderControl { get; set; } = string.Empty;
    }
}
