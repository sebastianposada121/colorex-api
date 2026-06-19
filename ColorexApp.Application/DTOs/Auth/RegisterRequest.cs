namespace ColorexApp.Application.DTOs.Auth;

public sealed record RegisterRequest(string Email, string Password, string FirstName, string LastName, string Role, Guid BranchId);
