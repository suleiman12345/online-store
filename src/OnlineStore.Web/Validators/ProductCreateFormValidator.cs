// <copyright file="ProductCreateFormValidator.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Validators;

using FluentValidation;
using OnlineStore.Web.Models;

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
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        this.RuleFor(x => x.Price)
            .GreaterThan(0);

        this.RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
