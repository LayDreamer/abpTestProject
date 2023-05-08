using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LocalTest.Data;

/* This is used if database provider does't define
 * ILocalTestDbSchemaMigrator implementation.
 */
public class NullLocalTestDbSchemaMigrator : ILocalTestDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
