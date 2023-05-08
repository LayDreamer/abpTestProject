using System.Threading.Tasks;

namespace LocalTest.Data;

public interface ILocalTestDbSchemaMigrator
{
    Task MigrateAsync();
}
