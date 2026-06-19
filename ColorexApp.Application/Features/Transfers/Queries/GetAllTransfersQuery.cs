using ColorexApp.Application.DTOs.Transfers;
using MediatR;

namespace ColorexApp.Application.Features.Transfers.Queries;

public sealed record GetAllTransfersQuery : IRequest<List<TransferResponse>>;
