using CatalogManagement.Domain.Entities;
using Common.ORMCommon;
using AutoMapper;

namespace CatalogManagement.Application.Branches.GetBranches;

public class GetBranchesProfile : Profile
{
    public GetBranchesProfile()
    {
        CreateMap<Branch, GetBranchesResponse>();
        CreateMap<GetBranchesQuery, PaginatedRequest>();
    }
}
