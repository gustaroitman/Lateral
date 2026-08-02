namespace Lateral.Domain.Exceptions;

public class EntityNotFoundException : RepositoryException
{
    public EntityNotFoundException(string entityName, Guid id)
        : base($"{entityName} with id '{id}' was not found.") { }

    public EntityNotFoundException(string entityName, string field, string value)
        : base($"{entityName} with {field} '{value}' was not found.") { }
}
