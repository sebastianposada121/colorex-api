using ColorexApp.Application.Features.Products.Commands;
using FluentValidation;

namespace ColorexApp.Application.Validators.Products;

public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.BaseReference).NotEmpty().MaximumLength(50);
        RuleFor(x => x.BasePrice).GreaterThan(0);
        RuleFor(x => x.Variants).NotEmpty();
        RuleForEach(x => x.Variants).ChildRules(v =>
        {
            v.RuleFor(x => x.Size).NotEmpty().MaximumLength(10);
            v.RuleFor(x => x.Color).NotEmpty().MaximumLength(50);
            v.RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        });
    }
}
