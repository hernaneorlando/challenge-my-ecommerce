using CatalogManagement.Domain.Entities;

namespace CatalogManagement.Domain.Events;

public class SupplierRegisteredEvent
{
    public Supplier Supplier { get; }

    public SupplierRegisteredEvent(Supplier supplier)
    {
        Supplier = supplier;
    }
}
