// <copyright file="ProductUpdateValidator.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Validators;

using FluentValidation;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Constants;

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
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(EntityConstraints.NameMaxLength);

        this.RuleFor(x => x.Price)
            .GreaterThan(0);

        this.RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
