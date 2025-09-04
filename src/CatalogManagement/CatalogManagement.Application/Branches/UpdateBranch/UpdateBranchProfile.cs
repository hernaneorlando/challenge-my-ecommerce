using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Branches.UpdateBranch;

/// <summary>
/// Profile for mapping between Branch entity and UpdateBranchResponse
/// </summary>
public class UpdateBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateBranch operation
    /// </summary>
    public UpdateBranchProfile()
    {
        CreateMap<UpdateBranchCommand, Branch>();
        CreateMap<Branch, UpdateBranchResponse>();
    }
}
