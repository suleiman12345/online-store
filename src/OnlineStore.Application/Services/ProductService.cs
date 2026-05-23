using OnlineStore.Contracts.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Сервис работы с товарами.
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        var categoryNames = categories.ToDictionary(x => x.Id, x => x.Name);

        return products.Select(x => MapToDto(x, categoryNames.GetValueOrDefault(x.CategoryId))).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);

        if (product == null)
            return null;

        var category = await _categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken);

        return MapToDto(product, category?.Name);
    }

    public async Task<Guid> CreateAsync(ProductCreateDto dto, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId, cancellationToken);

        if (category == null)
            throw new KeyNotFoundException("Category not found");

        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _productRepository.AddAsync(entity, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _productRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException("Product not found");

        entity.Name = dto.Name;
        entity.Price = dto.Price;
        entity.CategoryId = dto.CategoryId;

        _productRepository.Update(entity);
        await _productRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _productRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return;

        _productRepository.Remove(entity);
        await _productRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetByCategoryIdAsync(categoryId, cancellationToken);

        return products.Select(x => MapToDto(x, x.Category?.Name)).ToList();
    }

    private static ProductDto MapToDto(Product product, string? categoryName)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = categoryName ?? string.Empty,
        };
    }
}