using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Branches.GetBranchById;

/// <summary>
/// Profile for mapping between Branch entity and GetBranchByIdResponse
/// </summary>
public class GetBranchByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetBranchById operation
    /// </summary>
    public GetBranchByIdProfile()
    {
        CreateMap<Branch, GetBranchByIdResponse>();
    }
}
