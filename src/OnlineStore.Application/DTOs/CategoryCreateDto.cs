namespace OnlineStore.Application.DTOs;

/// <summary>
/// Payload for creating a category.
/// </summary>
public class CategoryCreateDto
{
    /// <summary>Category name.</summary>
    public string Name { get; set; } = string.Empty;
}
