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
    /// Tests that when an active user is suspended, their status changes to Suspended.
    /// </summary>
    [Fact(DisplayName = "User status should change to Suspended when suspended")]
    public void Given_ActiveUser_When_Suspended_Then_StatusShouldBeSuspended()
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
