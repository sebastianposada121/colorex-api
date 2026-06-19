using ColorexApp.Application.DTOs.Transfers;
using MediatR;

namespace ColorexApp.Application.Features.Transfers.Queries;

public sealed record GetTransfersByBranchQuery(Guid BranchId) : IRequest<List<TransferResponse>>;
