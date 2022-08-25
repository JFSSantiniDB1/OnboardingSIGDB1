using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.IOC;

namespace OnboardingSIGDB1.Test;

public class StartupTests
{
    public IConfiguration Configuration { get; }
    
    public StartupTests(IHostingEnvironment env)
    {
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        StartupIoc.ConfigureServices(services, Configuration);
    }


    public void Configure(IApplicationBuilder app)
    {
        
    }
}