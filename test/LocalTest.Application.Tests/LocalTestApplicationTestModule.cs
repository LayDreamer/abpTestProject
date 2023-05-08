using Volo.Abp.Modularity;

namespace LocalTest;

[DependsOn(
    typeof(LocalTestApplicationModule),
    typeof(LocalTestDomainTestModule)
    )]
public class LocalTestApplicationTestModule : AbpModule
{

}
