using System.Net.Http.Json;
using Common.APICommon;
using SalesManagement.Application.Services.DTOs;

namespace SalesManagement.Application.Services.ServiceImpl;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.CatalogClient);
    }

    public async Task<BranchDto> GetBranchDetailsAsync(Guid branchId)
    {
        var response = await _httpClient.GetAsync($"branches/{branchId}");
        response.EnsureSuccessStatusCode();

        var responseWithData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<BranchDto>>();
        return responseWithData?.Data!;
    }

    public async Task<ICollection<ProductDto>> GetProductDetailsAsync(ICollection<Guid> productsIds)
    {
        var filter = string.Join('&', productsIds.Select(id => $"id={id}"));
        var queryString = $"_page=1&_size={productsIds.Count}&{filter}";

        var response = await _httpClient.GetAsync($"products?{queryString}");
        response.EnsureSuccessStatusCode();

        var responseWithData = await response.Content.ReadFromJsonAsync<PaginatedResponse<ProductDto>>();
        return responseWithData?.Data!.ToArray()!;
    }

    public async Task<ICollection<SupplierDto>> GetSupplierDetailsAsync(ICollection<Guid> suppliersIds)
    {
        var filter = string.Join('&', suppliersIds.Select(id => $"id={id}"));
        var queryString = $"_page=1&_size={suppliersIds.Count}&{filter}";

        var response = await _httpClient.GetAsync($"suppliers?{queryString}");
        response.EnsureSuccessStatusCode();

        var responseWithData = await response.Content.ReadFromJsonAsync<PaginatedResponse<SupplierDto>>();
        return responseWithData?.Data!.ToArray()!;
    }
}
