using FluentValidation;
using MediatR;

namespace CatalogManagement.Application.Suppliers.CreateSupplier;

/// <summary>
/// Command for creating a new supplier.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a supplier, 
/// including name, registration number, email, and phone number. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSupplierResponse"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSupplierValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public record CreateSupplierCommand : IRequest<CreateSupplierResponse>
{
    /// <summary>
    /// Gets or sets the name of the supplier to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the registration number for the supplier.
    /// </summary>
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the supplier.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number for the supplier.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
