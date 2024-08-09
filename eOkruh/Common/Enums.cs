namespace eOkruh.Common
{
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

    public enum StructureTypes : ushort
    {
        Branch,
        Platoon,
        Company,
        Base,
        Division,
        Corps,
        Brigade,
        Army
    }

    public enum UserRoles : ushort
    {
        Viewer,
        Operator,
        Administrator,
        Owner
    }
}
