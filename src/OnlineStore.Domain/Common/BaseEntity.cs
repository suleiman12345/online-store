namespace OnlineStore.Domain.Common;

/// <summary>
/// Базовая сущность для всех таблиц.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }
}
