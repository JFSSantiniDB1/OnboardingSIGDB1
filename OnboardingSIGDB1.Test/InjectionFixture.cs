using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.IOC;

namespace OnboardingSIGDB1.Test;

public class InjectionFixture : IDisposable
{
    private readonly TestServer _server;

    public InjectionFixture()
    {
        _server = new TestServer(new WebHostBuilder().UseStartup<StartupTests>());
    }

    public IServiceProvider ServiceProvider => _server.Host.Services;

    public void Dispose()
    {
        _server.Dispose();
    }
}