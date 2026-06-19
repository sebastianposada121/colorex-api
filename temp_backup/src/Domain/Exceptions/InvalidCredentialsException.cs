namespace ColorexApp.Domain.Exceptions;

public sealed class InvalidCredentialsException()
    : DomainException("Invalid email or password.");
