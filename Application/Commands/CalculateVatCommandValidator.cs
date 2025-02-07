using FluentValidation;

namespace Application.Commands;

public class CalculateVatCommandValidator : AbstractValidator<CalculateVatCommand>
{
    public CalculateVatCommandValidator()
    {
        RuleFor(x => x.VatRate)
            .GreaterThan(0).WithMessage("VAT rate must be greater than 0.");

        RuleFor(x => x)
            .Must(x => new[] { x.NetAmount, x.GrossAmount, x.VatAmount }.Count(v => v.HasValue) == 1)
            .WithMessage("Exactly one amount (Net, Gross, or VAT) must be provided.");
    }
}