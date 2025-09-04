using CatalogManagement.Domain.Entities;
using AutoMapper;

namespace CatalogManagement.Application.Branches.CreateBranch;

/// <summary>
/// Profile for mapping between Branch entity and CreateBranch
/// </summary>
public class CreateBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateBranch operation
    /// </summary>
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Branch>();
        CreateMap<Branch, CreateBranchResponse>();
    }
}