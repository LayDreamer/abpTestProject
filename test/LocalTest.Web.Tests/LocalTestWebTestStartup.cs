using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace LocalTest;

public class LocalTestWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<LocalTestWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
