using Common.APICommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.Application.Services;
using SalesManagement.Application.Services.ServiceImpl;

namespace SalesManagement.Application;

public static class DependencyResolver
{
    public static WebApplicationBuilder AddHttpClients(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<PropagateTokenHandler>();

        builder.Services.AddHttpClient(HttpClientNames.CatalogClient, client =>
        {
            var baseAddress = builder.Configuration["ServiceEndpoints:CatalogManagement"]!;
            client.BaseAddress = new Uri(baseAddress + "/api/");
        }).AddHttpMessageHandler<PropagateTokenHandler>();

        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<IUserService, UserService>();

        return builder;
    }
}
