namespace OnlineStore.Application.DTOs;

/// <summary>
/// Payload for updating a category.
/// </summary>
public class CategoryUpdateDto
{
    /// <summary>Category name.</summary>
    public string Name { get; set; } = string.Empty;
}
