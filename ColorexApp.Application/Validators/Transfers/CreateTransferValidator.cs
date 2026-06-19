using ColorexApp.Application.Features.Transfers.Commands;
using FluentValidation;

namespace ColorexApp.Application.Validators.Transfers;

public sealed class CreateTransferValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferValidator()
    {
        RuleFor(x => x.OriginBranchId).NotEmpty();
        RuleFor(x => x.DestinationBranchId).NotEmpty();
        RuleFor(x => x.ProductVariantId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
