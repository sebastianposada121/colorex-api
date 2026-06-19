using System.Text.RegularExpressions;

namespace ColorexApp.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException($"'{value}' is not a valid email address.", nameof(value));
        Value = value.ToLowerInvariant();
    }

    public override string ToString() => Value;
    public static implicit operator string(Email email) => email.Value;
}
