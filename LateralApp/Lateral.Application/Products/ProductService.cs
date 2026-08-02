using Lateral.Domain.Entities;
using Lateral.Domain.Interfaces;

namespace Lateral.Application.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        return products.Select(ToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(id, cancellationToken);
        return product is null ? null : ToDto(product);
    }

    public async Task AddAsync(ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            IsActive = dto.IsActive
        };
        await productRepository.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            IsActive = dto.IsActive
        };
        SetId(entity, dto.Id);
        await productRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => productRepository.DeleteAsync(id, cancellationToken);

    private static ProductDto ToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        Quantity = p.Quantity,
        IsActive = p.IsActive
    };

    private static void SetId(Product entity, Guid id)
    {
        typeof(Product).BaseType!
            .GetProperty(nameof(Product.Id))!
            .SetValue(entity, id);
    }
}
