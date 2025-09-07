using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Common.ApiTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var realAuthService = services.SingleOrDefault(d => d.ServiceType == typeof(IAuthenticationService));
            if (realAuthService is not null)
            {
                services.Remove(realAuthService);
            }

            services.AddAuthentication(TestAuthHandlerConstants.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandlerConstants.AuthenticationScheme, options => { });
        });
    }
}