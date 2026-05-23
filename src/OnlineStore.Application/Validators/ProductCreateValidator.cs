// <copyright file="ProductCreateValidator.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Validators;

using FluentValidation;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Constants;

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
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(EntityConstraints.NameMaxLength);

        this.RuleFor(x => x.Price)
            .GreaterThan(0);

        this.RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
