using MediatR;

namespace ColorexApp.Application.Features.Branches.Commands;

public sealed record CreateBranchCommand(string Name, string Code, string DianResolutionPrefix, string DianRangeStart, string DianRangeEnd) : IRequest<Guid>;
