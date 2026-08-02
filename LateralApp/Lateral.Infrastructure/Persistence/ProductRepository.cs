using Lateral.Domain.Entities;
using Lateral.Domain.Exceptions;
using Lateral.Domain.Interfaces;

namespace Lateral.Infrastructure.Persistence;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private const int SimulatedLatencyMs = 500;
    private readonly List<Product> _products = [];

    public override async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new RepositoryException("Id cannot be empty.");

        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products;
    }

    public override async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (_products.Any(p => p.Name.Equals(entity.Name, StringComparison.OrdinalIgnoreCase)))
            throw new DuplicateEntityException(nameof(Product), nameof(Product.Name), entity.Name);

        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        _products.Add(entity);
    }

    public override async Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var index = _products.FindIndex(p => p.Id == entity.Id);
        if (index < 0)
            throw new EntityNotFoundException(nameof(Product), entity.Id);

        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        _products[index] = entity;
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new RepositoryException("Id cannot be empty.");

        if (!_products.Any(p => p.Id == id))
            throw new EntityNotFoundException(nameof(Product), id);

        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        _products.RemoveAll(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products.Where(p => p.IsActive);
    }

    public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RepositoryException("Name cannot be null or empty.");

        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
