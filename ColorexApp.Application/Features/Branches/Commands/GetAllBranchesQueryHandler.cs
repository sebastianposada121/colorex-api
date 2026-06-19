using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.DTOs.Branches;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Application.Features.Branches.Commands;

internal sealed class GetAllBranchesQueryHandler(IDbContext context) : IRequestHandler<GetAllBranchesQuery, List<BranchResponse>>
{
    public async Task<List<BranchResponse>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        return await context.Branches
            .Where(b => b.IsActive)
            .Select(b => new BranchResponse(b.Id, b.Name, b.Code, b.DianResolutionPrefix, b.DianRangeStart, b.DianRangeEnd, b.IsActive))
            .ToListAsync(cancellationToken);
    }
}
