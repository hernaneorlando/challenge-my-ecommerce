using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SalesManagement.Application.Repositories;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Sales.UpdateSale;

public class UpdateSaleHandler(
    ISaleRepository _saleRepository,
    ICatalogService _catalogService,
    IMapper _mapper) : IRequestHandler<UpdateSaleCommand, UpdateSaleResponse>
{
    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ValidationException([new ValidationFailure(string.Empty, $"The Sale with ID {request.Id} does not exist.")]);

        var productsToUpdate = sale.Items
            .Select(s => s.Product.Id)
            .Union(request.Products.Select(p => p.ProductId));

        var products = await _catalogService.GetProductDetailsAsync([.. productsToUpdate]);
        var suppliers = (await _catalogService.GetSupplierDetailsAsync([.. products.Select(p => p.SupplierId)]))
            .ToDictionary(supplier => supplier.Id, supplier => supplier);
        var newProducts = request.Products
            .ToDictionary(p => p.ProductId, p => p);

        var saleProducts = products.Select(product =>
            _mapper.Map<SaleItem>(product, opt => opt.AfterMap((_, saleItem) =>
            {
                var newProductValues = newProducts[product.Id];
                var supplierDto = suppliers[product.SupplierId];
                var saleProduct = _mapper.Map<SaleProduct>(product);
                var saleSupplier = _mapper.Map<SaleSupplier>(supplierDto);
                saleItem.Update(saleProduct, saleSupplier, newProductValues.Quantity, newProductValues.UnitPrice);
            }))
        ).ToList();

        sale.Update(saleProducts, request.Date, request.CancelSale ?? false);

        var createdSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        return _mapper.Map<UpdateSaleResponse>(createdSale);
    }
}
