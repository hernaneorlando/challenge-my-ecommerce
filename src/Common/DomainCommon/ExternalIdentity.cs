using System;

namespace Common.DomainCommon;

public class ExternalIdentity : IComparable<ExternalIdentity>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    public int CompareTo(ExternalIdentity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }
}
