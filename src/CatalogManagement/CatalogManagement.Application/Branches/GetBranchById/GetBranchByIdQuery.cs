using MediatR;

namespace CatalogManagement.Application.Branches.GetBranchById;

/// <summary>
/// Command for retrieving a Branch by their ID
/// </summary>
public record class GetBranchByIdQuery(Guid Id) : IRequest<GetBranchByIdResponse>;