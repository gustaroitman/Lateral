namespace Lateral.Domain.Entities;

/// <summary>
/// Clase base para todas las entidades del dominio.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    protected BaseEntity() { }

    protected BaseEntity(Guid id) => Id = id;
}
