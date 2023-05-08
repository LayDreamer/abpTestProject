using EasyAbp.PrivateMessaging;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using EasyAbp.Abp.Trees;

namespace LocalTest;

[DependsOn(
    typeof(LocalTestDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(LocalTestApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(PrivateMessagingApplicationModule),
    typeof(AbpTreesApplicationModule)
    )]
public class LocalTestApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<LocalTestApplicationModule>();
            //​options.ConventionalControllers.Create(typeof(PrivateMessagingApplicationModule).Assembly);​
        });
    }
}
