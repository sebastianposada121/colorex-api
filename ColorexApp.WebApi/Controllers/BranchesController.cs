using ColorexApp.Application.DTOs.Branches;
using ColorexApp.Application.Features.Branches.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorexApp.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/branches")]
public sealed class BranchesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllBranchesQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBranchCommand(
            request.Name, request.Code, request.DianResolutionPrefix,
            request.DianRangeStart, request.DianRangeEnd);
        var id = await sender.Send(command, cancellationToken);
        return Ok(new { id });
    }
}
