using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LocalTest.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class LocalTestDbContextFactory : IDesignTimeDbContextFactory<LocalTestDbContext>
{
    public LocalTestDbContext CreateDbContext(string[] args)
    {
        LocalTestEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<LocalTestDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("Default")));
        //new MySqlServerVersion(new Version("8.0.31")));
        return new LocalTestDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LocalTest.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
