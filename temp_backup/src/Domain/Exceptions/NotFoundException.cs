namespace ColorexApp.Domain.Exceptions;

public sealed class NotFoundException(string entityName, Guid id)
    : DomainException($"{entityName} with id '{id}' was not found.");
