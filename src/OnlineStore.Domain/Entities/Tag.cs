using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Product tag for classification and filtering.
/// </summary>
public class Tag : BaseEntity
{
    /// <summary>
    /// Tag display name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Products associated with this tag.
    /// </summary>
    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
