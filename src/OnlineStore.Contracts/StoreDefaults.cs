namespace OnlineStore.Contracts;

/// <summary>
/// Shared store defaults.
/// </summary>
public static class StoreDefaults
{
    /// <summary>Guest user identifier for session-based orders.</summary>
    public static readonly Guid GuestUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    /// <summary>Default guest cart created during database seeding.</summary>
    public static readonly Guid DefaultCartId = Guid.Parse("22222222-2222-2222-2222-222222222222");
}
