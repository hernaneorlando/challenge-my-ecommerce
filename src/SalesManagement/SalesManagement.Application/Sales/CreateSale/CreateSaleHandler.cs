using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SalesManagement.Application.Repositories;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Events;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Application.Sales.CreateSale;

public class CreateSaleHandler(
    ISaleRepository _saleRepository,
    ICartRepository _cartRepository,
    ICatalogService _catalogService,
    IMapper _mapper) : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
{
    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken)
            ?? throw new ValidationException([new ValidationFailure(string.Empty, $"The Cart with ID {request.CartId} does not exist.")]);

        var sale = _mapper.Map<Sale>(cart);

        var products = await _catalogService.GetProductDetailsAsync([.. cart.Items.Select(p => p.ProductId)]);
        var suppliers = (await _catalogService.GetSupplierDetailsAsync([.. cart.Items.Select(p => p.SupplierId)]))
            .ToDictionary(supplier => supplier.Id, supplier => supplier);

        foreach (var product in products)
        {
            var supplierDto = suppliers[product.SupplierId];
            var saleProduct = _mapper.Map<SaleProduct>(product);
            var saleSupplier = _mapper.Map<SaleSupplier>(supplierDto);
            sale.AddItem(saleProduct, saleSupplier);
        }

        sale.Create();

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResponse>(createdSale);
        result.AddDomainEvent(new SaleCreatedEvent(cart.Id, sale.Id));
        return result;
    }
}
