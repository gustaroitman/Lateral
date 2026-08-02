using Lateral.Domain.Interfaces;

namespace Lateral.Infrastructure.Persistence;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public virtual Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public virtual Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
