using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Tests.Common.ApiTests;

public abstract class BaseApiTests<TProgram, TDbContext> : IClassFixture<WebApplicationFactory<TProgram>>, IDisposable
    where TProgram : class
    where TDbContext : DbContext
{
    protected readonly WebApplicationFactory<TProgram> _webFactory;

    protected BaseApiTests(WebApplicationFactory<TProgram> factory)
    {
        _webFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("IntegrationTests");
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<TDbContext>>();
                services.RemoveAll<TDbContext>();
                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                }, ServiceLifetime.Singleton);
            });
        });
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}