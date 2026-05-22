namespace OnlineStore.Application.DTOs;

/// <summary>
/// Category data transfer object.
/// </summary>
public class CategoryDto
{
    /// <summary>Category identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Category name.</summary>
    public string Name { get; set; } = string.Empty;
}
