using AutoMapper;
using Common.ORMCommon;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Sales.GetSales;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap<Sale, GetSalesResponse>();
        CreateMap<GetSalesQuery, PaginatedRequest>();
    }
}
