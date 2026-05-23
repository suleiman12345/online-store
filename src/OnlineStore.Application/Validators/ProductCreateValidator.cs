using FluentValidation;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Application.Validators;

/// <summary>
/// Validation rules for <see cref="ProductCreateDto"/>.
/// </summary>
public class ProductCreateValidator : AbstractValidator<ProductDto>
{
    /// <summary>
    /// Initializes validation rules for product creation.
    /// </summary>
    public ProductCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required.");
    }
}
