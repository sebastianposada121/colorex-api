using ColorexApp.Application.DTOs.Auth;
using ColorexApp.Application.Features.Auth.Commands;
using ColorexApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ColorexApp.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var token = await sender.Send(command, cancellationToken);

        return Ok(new LoginResponse(token));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var role = Enum.Parse<UserRole>(request.Role);

        var command = new RegisterCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            role,
            request.BranchId);

        var userId = await sender.Send(command, cancellationToken);

        return Ok(new RegisterResponse(userId));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var role = Enum.Parse<UserRole>(request.Role);

        var command = new UpdateUserCommand(
            id,
            request.FirstName,
            request.LastName,
            role,
            request.BranchId);

        await sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await sender.Send(command, cancellationToken);

        return NoContent();
    }
}
