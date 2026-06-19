using ColorexApp.Application.Features.Invoices.Commands;
using FluentValidation;

namespace ColorexApp.Application.Validators.Invoices;

public sealed class CreateInvoiceValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceValidator()
    {
        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.Items).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(i =>
        {
            i.RuleFor(x => x.Sku).NotEmpty();
            i.RuleFor(x => x.Quantity).GreaterThan(0);
            i.RuleFor(x => x.UnitPrice).GreaterThan(0);
        });
    }
}
