namespace Common.DomainCommon.ValueObjects;

public sealed class PhoneNumber(string value) : ValueObject
{
    /// <summary>
    /// Gets the entity's phone number.
    /// Must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public string Value { get; set; } = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber?.Value ?? string.Empty;
}