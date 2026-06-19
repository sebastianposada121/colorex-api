using ColorexApp.Application.Features.Dashboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorexApp.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController(ISender sender) : ControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdminSummary(CancellationToken cancellationToken)
    {
        var query = new GetAdminSummaryQuery();
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetBranchSummary(Guid branchId, CancellationToken cancellationToken)
    {
        var query = new GetBranchSummaryQuery(branchId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
