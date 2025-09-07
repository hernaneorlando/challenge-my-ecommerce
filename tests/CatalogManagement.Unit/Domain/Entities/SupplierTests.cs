using CatalogManagement.Unit.Domain.Entities.TestData;
using Common.DomainCommon.ValueObjects;
using Tests.Common;

namespace CatalogManagement.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Supplier entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SupplierTests
{
    /// <summary>
    /// Tests the update of a supplier.
    /// </summary>
    [Fact(DisplayName = "Supplier should get the new property values when updated")]
    public void Given_ASupplier_When_Updated_Then_GetTheNewPropertyValues()
    {
        // Arrange
        const string newPhone = "+55 11 91234-5678";

        var existentSupplier = SupplierTestData.GenerateValidSupplier();
        var updatedSupplier = ObjectUtil.Copy(existentSupplier)!;
        updatedSupplier.Phone = new PhoneNumber(newPhone);

        // Act
        existentSupplier.Update(updatedSupplier);

        // Assert
        Assert.Equal(existentSupplier, updatedSupplier);
        Assert.Equal(newPhone, existentSupplier.Phone!);
    }
}
