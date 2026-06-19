using ColorexApp.Application.DTOs.Transfers;
using ColorexApp.Application.Features.Transfers.Commands;
using ColorexApp.Application.Features.Transfers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorexApp.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/transfers")]
public sealed class TransfersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllTransfersQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTransferByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetByBranch(Guid branchId, CancellationToken cancellationToken)
    {
        var query = new GetTransfersByBranchQuery(branchId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransferRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTransferCommand(
            request.OriginBranchId, request.DestinationBranchId,
            request.ProductVariantId, request.Quantity);
        var id = await sender.Send(command, cancellationToken);
        return Ok(new { id });
    }

    [HttpPut("{id:guid}/receive")]
    public async Task<IActionResult> ConfirmReception(Guid id, [FromBody] ConfirmReceptionRequest request, CancellationToken cancellationToken)
    {
        var command = new ConfirmReceptionCommand(id, request.ReceivedQuantity);
        await sender.Send(command, cancellationToken);
        return NoContent();
    }
}
