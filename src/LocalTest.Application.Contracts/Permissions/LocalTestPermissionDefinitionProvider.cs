using LocalTest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LocalTest.Permissions;

public class LocalTestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LocalTestPermissions.GroupName, L("Permission:LocalTest"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LocalTestPermissions.MyPermission1, L("Permission:MyPermission1"));

        var familyLibsPermission = myGroup.AddPermission(LocalTestPermissions.FamilyLibs.Default, L("Permission:FamilyLibs"));
        familyLibsPermission.AddChild(LocalTestPermissions.FamilyLibs.Delete, L("Permission:FamilyLibs.Delete"));

        var projectsPermission = myGroup.AddPermission(LocalTestPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(LocalTestPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectsPermission.AddChild(LocalTestPermissions.Projects.Edit, L("Permission:Projects.Edit"));
        projectsPermission.AddChild(LocalTestPermissions.Projects.Delete, L("Permission:Projects.Delete"));

        var materialSpecificationListPermission = myGroup.AddPermission(LocalTestPermissions.MaterialSpecificationList.Default, L("Permission:MaterialSpecificationList"));
        materialSpecificationListPermission.AddChild(LocalTestPermissions.MaterialSpecificationList.Create, L("Permission:MaterialSpecificationList.Create"));
        materialSpecificationListPermission.AddChild(LocalTestPermissions.MaterialSpecificationList.Delete, L("Permission:MaterialSpecificationList.Delete"));

        var factoryListPermission = myGroup.AddPermission(LocalTestPermissions.FactoryMaterialRequisition.Default, L("Permission:FactoryMaterialRequisition"));
        factoryListPermission.AddChild(LocalTestPermissions.FactoryMaterialRequisition.Create, L("Permission:FactoryMaterialRequisition.Create"));
        factoryListPermission.AddChild(LocalTestPermissions.FactoryMaterialRequisition.Delete, L("Permission:FactoryMaterialRequisition.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LocalTestResource>(name);
    }
}
