namespace ColorexApp.Domain.Exceptions;

public sealed class DuplicateEmailException(string email)
    : DomainException($"The email '{email}' is already registered.");
