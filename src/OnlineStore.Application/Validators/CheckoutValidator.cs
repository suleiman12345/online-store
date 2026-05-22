using FluentValidation;
using OnlineStore.Application.DTOs;
using OnlineStore.Domain.Constants;

namespace OnlineStore.Application.Validators;

/// <summary>
/// Validation rules for <see cref="CheckoutDto"/>.
/// </summary>
public class CheckoutValidator : AbstractValidator<CheckoutDto>
{
    /// <summary>
    /// Initializes validation rules for checkout.
    /// </summary>
    public CheckoutValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(EntityConstraints.EmailMaxLength);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(EntityConstraints.FullNameMaxLength);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(EntityConstraints.PhoneMaxLength);

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty()
            .MaximumLength(EntityConstraints.AddressMaxLength);
    }
}
