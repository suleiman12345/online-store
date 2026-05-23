using FluentValidation;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Application.Validators;

/// <summary>
/// Validation rules for <see cref="OrderDto"/>.
/// </summary>
public class OrderCreateValidator : AbstractValidator<OrderDto>
{
    /// <summary>
    /// Initializes validation rules for order creation.
    /// </summary>
    public OrderCreateValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Order must contain at least one item.");

        RuleForEach(x => x.Items).ChildRules(line =>
        {
            line.RuleFor(l => l.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            line.RuleFor(l => l.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        });
    }
}
