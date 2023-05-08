namespace LocalTest.Permissions;

public static class LocalTestPermissions
{
    public const string GroupName = "LocalTest";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class FamilyLibs
    {
        public const string Default = GroupName + ".FamilyLibs";
        public const string Delete = Default + ".Delete";
    }

    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class MaterialSpecificationList
    {
        public const string Default = GroupName + ".MaterialSpecificationList";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class FactoryMaterialRequisition
    {
        public const string Default = GroupName + ".FactoryMaterialRequisition";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
