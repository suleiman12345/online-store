using FluentValidation;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Constants;

namespace OnlineStore.Application.Validators;

/// <summary>
/// Validation rules for product updates.
/// </summary>
public class ProductUpdateValidator : AbstractValidator<ProductDto>
{
    /// <summary>
    /// Initializes validation rules for product updates.
    /// </summary>
    public ProductUpdateValidator()
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
