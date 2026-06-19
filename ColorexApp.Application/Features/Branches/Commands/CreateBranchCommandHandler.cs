using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Domain.Entities;
using MediatR;

namespace ColorexApp.Application.Features.Branches.Commands;

internal sealed class CreateBranchCommandHandler(IBranchRepository branchRepository) : IRequestHandler<CreateBranchCommand, Guid>
{
    public async Task<Guid> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = Branch.Create(request.Name, request.Code, request.DianResolutionPrefix, request.DianRangeStart, request.DianRangeEnd);
        await branchRepository.AddAsync(branch, cancellationToken);
        return branch.Id;
    }
}
