using System.Net.Http.Json;
using Common.APICommon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using SalesManagement.Application.Sales.CreateSale;
using SalesManagement.Application.Services;
using SalesManagement.Integration.Sales.TestData;
using SalesManagement.ORM;
using SalesManagement.WebApi;
using Tests.Common.ApiTests;
using FluentAssertions;

namespace SalesManagement.Integration.Sales;

public class CreateSaleFeatureTests(CustomWebApplicationFactory<Program> factory) : BaseApiTests<Program, DefaultContext>(factory)
{
    public ICatalogService? CatalogServiceMock { get; private set; }

    protected override void ChangeServices(IServiceCollection services)
    {
        CatalogServiceMock = Substitute.For<ICatalogService>();

        services.RemoveAll<ICatalogService>();
        services.AddSingleton(CatalogServiceMock);
    }

    [Fact]
    public async Task CreateSale_ValidCart_ReturnSuccess()
    {
        // Given
        var dbContext = _webFactory.Services
            .CreateScope()
            .ServiceProvider.GetService<DefaultContext>()!;

        var existentCart = CartFeatureTestData.GenerateValidCart();

        dbContext.Carts.Add(existentCart);
        await dbContext.SaveChangesAsync();

        var client = _webFactory.CreateClient();
        var request = new
        {
            CartId = existentCart.Id
        };

        CatalogServiceMock!.GetProductDetailsAsync(Arg.Any<ICollection<Guid>>())
            .Returns([.. existentCart.Items.Select(item => {
                var productDto = CartFeatureTestData.GenerateValidProductDto();
                productDto.Id = item.ProductId;
                productDto.SupplierId = item.SupplierId;
                return productDto;
            })]);

        // Setup para a chamada de Fornecedores
        CatalogServiceMock.GetSupplierDetailsAsync(Arg.Any<ICollection<Guid>>())
            .Returns([..existentCart.Items.Select(item =>
        {
            var supplierDto = CartFeatureTestData.GenerateValidSupplierDto();
            supplierDto.Id = item.SupplierId;
            return supplierDto;
        })]);

        // When
        var response = await client.PostAsJsonAsync("/api/sales", request);

        // Then
        response.EnsureSuccessStatusCode();
        var apiResponseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();

        Assert.NotNull(apiResponseData);
        Assert.True(apiResponseData.Success);
        Assert.NotNull(apiResponseData.Data);
        Assert.Equal("Sale created successfully", apiResponseData.Message);

        var sale = apiResponseData.Data;
        sale.Number.Should().NotBeEmpty();
        sale.Branch.Should().NotBe(null);
        sale.TotalAmount.Should().BeGreaterThan(0);
        sale.Status.Should().Be(Domain.Enums.SaleStatus.Pending);
        sale.CreatedAt.Should().NotBe(null);
        sale.Products.Should().NotBeEmpty();
    }
}