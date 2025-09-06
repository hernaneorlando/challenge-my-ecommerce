using CatalogManagement.Application.Suppliers.CreateSupplier;
using CatalogManagement.Domain.Entities;
using AutoMapper;
using NSubstitute;
using FluentAssertions;
using CatalogManagement.Unit.Application.Suppliers.TestData;
using Common.DomainCommon.ValueObjects;
using CatalogManagement.Application.Repositories;

namespace CatalogManagement.Unit.Application.Suppliers;

/// <summary>
/// Contains unit tests for the <see cref="CreateSupplierHandler"/> class.
/// </summary>
public class CreateSupplierHandlerTests
{
    private readonly ISupplierRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreateSupplierHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSupplierHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSupplierHandlerTests()
    {
        _repository = Substitute.For<ISupplierRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSupplierHandler(_repository, _mapper);
    }

    /// <summary>
    /// Tests that a valid supplier creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid supplier data When creating supplier Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSupplierHandlerTestData.GenerateValidCommand();
        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            RegistrationNumber = command.RegistrationNumber,
            Email = command.Email,
            Phone = new PhoneNumber(command.Phone),
        };

        var result = new CreateSupplierResponse
        {
            Id = supplier.Id,
        };

        _mapper.Map<Supplier>(command).Returns(supplier);
        _mapper.Map<CreateSupplierResponse>(supplier).Returns(result);

        _repository.CreateAsync(Arg.Any<Supplier>(), Arg.Any<CancellationToken>())
            .Returns(supplier);

        // When
        var createSupplierResponse = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSupplierResponse.Should().NotBeNull();
        createSupplierResponse.Id.Should().Be(supplier.Id);
        await _repository.Received(1).CreateAsync(Arg.Any<Supplier>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to supplier entity")]
    public async Task Handle_ValidRequest_MapsCommandToSupplier()
    {
        // Given
        var command = CreateSupplierHandlerTestData.GenerateValidCommand();
        var supplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            RegistrationNumber = command.RegistrationNumber,
            Email = command.Email,
            Phone = new PhoneNumber(command.Phone),
        };

        _mapper.Map<Supplier>(command).Returns(supplier);
        _repository.CreateAsync(Arg.Any<Supplier>(), Arg.Any<CancellationToken>())
            .Returns(supplier);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Supplier>(Arg.Is<CreateSupplierCommand>(c =>
            c.Name == command.Name &&
            c.RegistrationNumber == command.RegistrationNumber &&
            c.Email == command.Email &&
            c.Phone == command.Phone));
    }
}
