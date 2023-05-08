using LocalTest.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace LocalTest.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LocalTestEntityFrameworkCoreModule),
    typeof(LocalTestApplicationContractsModule)
    )]
public class LocalTestDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
