using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Product application service implementation.
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="ProductService"/>.
    /// </summary>
    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllWithCategoryAsync(cancellationToken);
        return products.Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id, cancellationToken);
        return product is null ? null : MapToDto(product);
    }

    /// <inheritdoc />
    public async Task<ProductDto> CreateAsync(ProductCreateDto dto, CancellationToken cancellationToken = default)
    {
        await EnsureCategoryExistsAsync(dto.CategoryId, cancellationToken);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId
        };

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        var created = await _productRepository.GetByIdWithCategoryAsync(product.Id, cancellationToken);
        return MapToDto(created!);
    }

    /// <inheritdoc />
    public async Task<ProductDto?> UpdateAsync(
        Guid id,
        ProductUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        await EnsureCategoryExistsAsync(dto.CategoryId, cancellationToken);

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        var updated = await _productRepository.GetByIdWithCategoryAsync(id, cancellationToken);
        return MapToDto(updated!);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return false;
        }

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task EnsureCategoryExistsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (category is null)
        {
            throw new InvalidOperationException($"Category with id '{categoryId}' was not found.");
        }
    }

    private static ProductDto MapToDto(Product product) =>
        new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? string.Empty,
            Tags = product.ProductTags
                .Select(pt => pt.Tag?.Name ?? string.Empty)
                .Where(n => !string.IsNullOrEmpty(n))
                .OrderBy(n => n)
                .ToList()
        };
}
