using FluentValidation;
using OnlineStore.Web.Models;

namespace OnlineStore.Web.Validators;

/// <summary>
/// Client-side validation for product create form.
/// </summary>
public class ProductCreateFormValidator : AbstractValidator<ProductCreateFormModel>
{
    /// <summary>
    /// Initializes validation rules.
    /// </summary>
    public ProductCreateFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
