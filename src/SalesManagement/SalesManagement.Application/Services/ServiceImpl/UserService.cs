using System.Net.Http.Json;
using System.Security.Claims;
using Common.APICommon;
using Microsoft.AspNetCore.Http;
using SalesManagement.Application.Services.DTOs;

namespace SalesManagement.Application.Services.ServiceImpl;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;

    public UserService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.CatalogClient);
    }

    public UserDto GetAuthenticatedUser()
    {
        var user = _httpContextAccessor.HttpContext!.User;
        return new UserDto
        {
            Id = Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId) ? userId : Guid.Empty,
            Name = user.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
            UserName = user.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
            Role = user.FindFirstValue(ClaimTypes.Role) ?? string.Empty
        };
    }

    public async Task<UserDto> GetUserDetailsAsync(Guid userId)
    {
        var authenticatedUser = GetAuthenticatedUser();
        if (authenticatedUser.Id == userId)
            return authenticatedUser;

        var response = await _httpClient.GetAsync($"users/{userId}");
        response.EnsureSuccessStatusCode();

        var responseWithData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<UserDto>>();
        return responseWithData?.Data!;
    }
}
