using AutoMapper;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Sales.GetSaleById;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleByIdResponse
/// </summary>
public class GetSaleByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleById operation
    /// </summary>
    public GetSaleByIdProfile()
    {
        CreateMap<Sale, GetSaleByIdResponse>()
            .ForMember(r => r.Products, opt => opt.MapFrom(c => c.Items));
    }
}
