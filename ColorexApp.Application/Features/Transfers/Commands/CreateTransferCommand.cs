using MediatR;

namespace ColorexApp.Application.Features.Transfers.Commands;

public sealed record CreateTransferCommand(Guid OriginBranchId, Guid DestinationBranchId, Guid ProductVariantId, int Quantity) : IRequest<Guid>;
