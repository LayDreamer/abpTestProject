using System.Threading.Tasks;
using LocalTest.Localization;
using LocalTest.MultiTenancy;
using LocalTest.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace LocalTest.Web.Menus;

public class LocalTestMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<LocalTestResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                LocalTestMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        // context.Menu.AddItem(
        //     new ApplicationMenuItem(
        //     "LocalTest",
        //     l["Menu:BookStore"],
        //     icon: "fa fa-book"
        //     ).AddItem(
        //         new ApplicationMenuItem(
        //             "LocalTest.Books",
        //             l["Menu:Books"],
        //             url: "/Books"
        //         )
        //     )
        // );

        // context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //    "LocalTest",
        //    l["Menu:FileList"],
        //    icon: "fa fa-book"
        //    ).AddItem(
        //        new ApplicationMenuItem(
        //            "LocalTest.Files",
        //            l["Menu:Files"],
        //            url: "/Files"
        //        )
        //    )
        //);

        // context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //    "LocalTest",
        //    l["Menu:FamilyList"],
        //    icon: "fa fa-book"
        //    ).AddItem(
        //        new ApplicationMenuItem(
        //            "LocalTest.Families",
        //            l["Menu:Families"],
        //            url: "/Families"
        //        )
        //    )
        //);

        #region 族库管理
        if (await context.IsGrantedAsync(LocalTestPermissions.FamilyLibs.Default))
        {
            context.Menu.AddItem(
          new ApplicationMenuItem(
          "LocalTest",
          l["Menu:FamilyList"],
          icon: "fa fa-warehouse"
          ).AddItem(
              new ApplicationMenuItem(
                  "LocalTest.FamilyLibs",
                  l["Menu:FamilyLibs"],
                  url: "/FamilyLibs"
              )
          ));
        }

        #endregion

        #region 项目列表
        if (await context.IsGrantedAsync(LocalTestPermissions.Projects.Default))
        {
            context.Menu.AddItem(
         new ApplicationMenuItem(
         "LocalTest",
         l["Menu:Projects"],
         icon: "fa fa-list"
         ).AddItem(
             new ApplicationMenuItem(
                 "LocalTest.Projects",
                 l["Menu:Project"],
                 url: "/Projects"
             )
         ));
        }
        #endregion


        #region 物料规格清单

        if (await context.IsGrantedAsync(LocalTestPermissions.MaterialSpecificationList.Default))
        {
            var materialSpecificationListMenu = new ApplicationMenuItem(
           "LocalTest",
           l["Menu:MaterialSpecificationList"],
           icon: "fa fa-sitemap"); /*fa - sitemap fa-outdent*/
            var materialSpecificationSet = new ApplicationMenuItem(
                       "LocalTest.MaterialSpecificationList",
                       l["Menu:MaterialSpecificationSet"],
                       url: "/MaterialSpecificationList"
                   );
            //var materialSpecificationDetail = new ApplicationMenuItem(
            //           "LocalTest.MaterialSpecificationDetail",
            //           l["Menu:MaterialSpecificationDetail"],
            //           url: "/MaterialSpecificationDetail"
            //       );

            materialSpecificationListMenu.AddItem(materialSpecificationSet);
            //materialSpecificationListMenu.AddItem(materialSpecificationDetail);
            context.Menu.AddItem(materialSpecificationListMenu);
        }
        #endregion


        #region 领料单列表
        if (await context.IsGrantedAsync(LocalTestPermissions.FactoryMaterialRequisition.Default))
        {
            context.Menu.AddItem(
         new ApplicationMenuItem(
         "LocalTest",
         l["Menu:FactoryList"],
         icon: "fa fa-list"
         ).AddItem(
             new ApplicationMenuItem(
                 "LocalTest.FactoryList",
                 l["Menu:RequisitionList"],
                 url: "/FactoryList"
             )
         ));
        }
        #endregion
    }
}
