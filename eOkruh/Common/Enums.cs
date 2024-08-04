namespace eOkruh.Common
{
    public enum UserRoles : ushort
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
    public enum OfficerPersonnel : ushort
    {
        JuniorLieutenant,
        Lieutenant,
        SeniorLieutenant,
        Captain,

        Major,
        LieutenantColonel,
        Colonel,

        BrigadeGeneral,
        GeneralMajor,
        GeneralLieutenant,
        General
    }

}
