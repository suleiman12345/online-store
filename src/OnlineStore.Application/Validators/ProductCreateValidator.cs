using FluentValidation;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Constants;

namespace OnlineStore.Application.Validators;

/// <summary>
/// Validation rules for <see cref="ProductCreateDto"/>.
/// </summary>
public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    /// <summary>
    /// Initializes validation rules for product creation.
    /// </summary>
    public ProductCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(EntityConstraints.NameMaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
