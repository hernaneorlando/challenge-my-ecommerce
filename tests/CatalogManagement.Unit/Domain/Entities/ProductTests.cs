using System;
using CatalogManagement.Unit.Domain.Entities.TestData;
using Common.Validations;
using Tests.Common;

namespace CatalogManagement.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class ProductTests
{
    [Fact(DisplayName = "Supplier should get the new property values when updated")]
    public void Given_ASupplier_When_Updated_Then_ShouldGetTheNewPropertyValues()
    {
        // Arrange
        const decimal newPrice = 15.25M;

        var existentSupplier = ProductTestData.GenerateValidProduct();
        var updatedSupplier = ObjectUtil.Copy(existentSupplier)!;
        updatedSupplier.Price = newPrice;

        // Act
        var result = existentSupplier.Update(updatedSupplier);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
        Assert.Equal(existentSupplier, updatedSupplier);
        Assert.Equal(newPrice, existentSupplier.Price!);
    }

    [Fact(DisplayName = "Supplier update should return validation erros because of invalid inputs")]
    public void Given_ASupplier_When_UpdatedWithInvalidInputs_Then_ShouldReturnValidationErrors()
    {
        // Arrange
        var existentSupplier = ProductTestData.GenerateValidProduct();
        var updatedSupplier = ObjectUtil.Copy(existentSupplier)!;
        updatedSupplier.Title = string.Empty;
        updatedSupplier.Price = 0;
        updatedSupplier.SupplierId = Guid.Empty;

        // Act
        var result = existentSupplier.Update(updatedSupplier);

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(3, result.Errors.Count());
        Assert.Collection(result.Errors, error =>
        {
            Assert.Equal("NotEmptyValidator", error.Error);
            Assert.Equal("'Title' deve ser informado.", error.Detail);
        },
        error =>
        {
            Assert.Equal("GreaterThanValidator", error.Error);
            Assert.Equal("Price must be greater than zero.", error.Detail);
        },
        error =>
        {
            Assert.Equal("NotEqualValidator", error.Error);
            Assert.Equal("SupplierId must be a valid GUID.", error.Detail);
        });
    }

    [Theory(DisplayName = "Supplier description must be at least 10 characters long and not exceed 256 characters")]
    [InlineData(9, "MinimumLengthValidator", "Description must be at least 10 characters long.")]
    [InlineData(257, "MaximumLengthValidator", "Description cannot be longer than 256 characters.")]
    public void Given_ASupplier_When_UpdatedWithInvalidDescription_Then_ShouldReturnValidationErrors(int size, string validatorName, string errorMessage)
    {
        // Arrange
        var existentSupplier = ProductTestData.GenerateValidProduct();
        var updatedSupplier = ObjectUtil.Copy(existentSupplier)!;
        updatedSupplier.Description = ProductTestData.GenerateMinimalInvalidDescription(size);

        // Act
        var result = existentSupplier.Update(updatedSupplier);

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);

        var expectedError = new ValidationErrorDetail(validatorName, errorMessage);
        Assert.Collection(result.Errors, error =>
        {
            Assert.Equal(validatorName, error.Error);
            Assert.Equal(errorMessage, error.Detail);
        });
    }
}
