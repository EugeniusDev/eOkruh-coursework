namespace eOkruh.Common
{
    public enum UserTypes : ushort
    {
        Viewer,
        Operator,
        Administrator,
        Owner
    }

    public enum StructureType : ushort
    {
        Base,// TODO remake or what
        Division,
        Army,
        Corps
    }

    public enum MilitarySpeciality : ushort
    {
        test//todo
    }

    public enum OrdinaryPersonnel : ushort
    {
        Recruit,
        Soldier,
        SeniorSoldier
    }
    public enum SergeantPersonnel : ushort
    {
        JuniorSergeant,
        Sergeant,
        SeniorSergeant,
        ChiefSergeant,
        StaffSergeant,
        MasterSergeant,
        SeniorMasterSergeant,
        ChiefMasterSergeant
    }
    public enum JuniorOfficerPersonnel : ushort
    {
        JuniorLieutenant,
        Lieutenant,
        SeniorLieutenant,
        Captain
    }
    public enum SeniorOfficerPersonnel : ushort
    {
        Major,
        LieutenantColonel,
        Colonel
    }
    public enum HigherOfficerPersonnel : ushort
    {
        BrigadeGeneral,
        GeneralMajor,
        GeneralLieutenant,
        General
    }

}
