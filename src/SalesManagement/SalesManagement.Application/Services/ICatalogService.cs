using SalesManagement.Application.Services.DTOs;

namespace SalesManagement.Application.Services;

public interface ICatalogService
{
    Task<BranchDto> GetBranchDetailsAsync(Guid branchId);
    Task<ICollection<ProductDto>> GetProductDetailsAsync(ICollection<Guid> productsIds);
    Task<ICollection<SupplierDto>> GetSupplierDetailsAsync(ICollection<Guid> suppliersIds);
}
