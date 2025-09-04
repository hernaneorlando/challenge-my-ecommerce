using Microsoft.AspNetCore.Builder;

namespace UserManagement.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
