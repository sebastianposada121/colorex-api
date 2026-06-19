using ColorexApp.Application.DTOs.Branches;
using MediatR;

namespace ColorexApp.Application.Features.Branches.Commands;

public sealed record GetAllBranchesQuery : IRequest<List<BranchResponse>>;
