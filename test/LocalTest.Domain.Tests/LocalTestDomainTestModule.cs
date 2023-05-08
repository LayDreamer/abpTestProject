using LocalTest.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace LocalTest;

[DependsOn(
    typeof(LocalTestEntityFrameworkCoreTestModule)
    )]
public class LocalTestDomainTestModule : AbpModule
{

}
