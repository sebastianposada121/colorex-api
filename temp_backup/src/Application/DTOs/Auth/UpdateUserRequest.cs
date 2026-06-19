namespace ColorexApp.Application.DTOs.Auth;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Role,
    Guid BranchId);
