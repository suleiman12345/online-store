namespace OnlineStore.Domain.Constants;

/// <summary>
/// Shared entity field constraints.
/// </summary>
public static class EntityConstraints
{
    /// <summary>Maximum length for short names.</summary>
    public const int NameMaxLength = 200;

    /// <summary>Maximum length for descriptions.</summary>
    public const int DescriptionMaxLength = 2000;

    /// <summary>Maximum length for email addresses.</summary>
    public const int EmailMaxLength = 320;

    /// <summary>Maximum length for full names.</summary>
    public const int FullNameMaxLength = 200;

    /// <summary>Decimal precision for monetary values.</summary>
    public const int PricePrecision = 18;

    /// <summary>Decimal scale for monetary values.</summary>
    public const int PriceScale = 2;

    /// <summary>Maximum length for phone numbers.</summary>
    public const int PhoneMaxLength = 32;

    /// <summary>Maximum length for addresses.</summary>
    public const int AddressMaxLength = 500;

    /// <summary>Maximum length for tag names.</summary>
    public const int TagNameMaxLength = 100;
}
