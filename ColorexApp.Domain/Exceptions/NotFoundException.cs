namespace ColorexApp.Domain.Exceptions;

public sealed class NotFoundException(string entityName, Guid id)
    : DomainException($"{entityName} with id '{id}' was not found.");

public sealed class NotFoundByKeyException(string entityName, string keyName, string keyValue)
    : DomainException($"{entityName} with {keyName} '{keyValue}' was not found.");
