using Lateral.Domain.Entities;
using Lateral.Domain.Interfaces;

namespace Lateral.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private const int SimulatedLatencyMs = 500;
    private readonly List<Product> _products = [];

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products;
    }

    public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        _products.Add(entity);
    }

    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        var index = _products.FindIndex(p => p.Id == entity.Id);
        if (index >= 0) _products[index] = entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
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
        await Task.Delay(SimulatedLatencyMs, cancellationToken);
        return _products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
