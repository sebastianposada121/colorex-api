using MediatR;

namespace ColorexApp.Application.Features.Transfers.Commands;

public sealed record ConfirmReceptionCommand(Guid TransferId, int ReceivedQuantity) : IRequest;
