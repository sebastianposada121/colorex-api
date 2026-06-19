namespace ColorexApp.Application.DTOs.Auth;

public sealed record LoginResponse(string Token, string Email, string FirstName, string LastName, string Role, Guid BranchId, Guid UserId);
