using AutoMapper;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<Sale, CreateSaleResponse>()
            .ForMember(r => r.Products, opt => opt.MapFrom(c => c.Items));
    }
}
