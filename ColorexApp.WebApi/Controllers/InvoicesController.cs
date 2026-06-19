using ColorexApp.Application.DTOs.Invoices;
using ColorexApp.Application.Features.Invoices.Commands;
using ColorexApp.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ColorexApp.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/invoices")]
public sealed class InvoicesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
        var branchIdClaim = User.FindFirstValue("branchId");

        if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Invalid user token" });

        if (branchIdClaim is null || !Guid.TryParse(branchIdClaim, out var branchId))
            return Unauthorized(new { error = "Invalid branch token" });

        var command = new CreateInvoiceCommand(
            branchId, userId, request.PaymentMethod,
            GetDianPrefix(branchId),
            request.Items.Select(i => new CreateInvoiceItemCommand(i.Sku, i.Quantity, i.UnitPrice)).ToList());

        var id = await sender.Send(command, cancellationToken);
        return Ok(new { id });
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetToday(CancellationToken cancellationToken)
    {
        var query = new GetTodayInvoicesQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    private string GetDianPrefix(Guid branchId)
    {
        // In a real app, look this up from the branch repository
        return "COLX";
    }
}
