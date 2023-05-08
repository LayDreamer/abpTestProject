using Localization.Resources.AbpUi;
using LocalTest.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using EasyAbp.PrivateMessaging;
using EasyAbp.Abp.Trees;

namespace LocalTest;

[DependsOn(
    typeof(LocalTestApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(PrivateMessagingHttpApiModule),
    typeof(AbpTreesHttpApiModule)
    )]
public class LocalTestHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<LocalTestResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
