using FluentValidation;

namespace Application.Commands;

public class CalculateVatCommandValidator : AbstractValidator<CalculateVatCommand>
{
    public CalculateVatCommandValidator()
    {
        RuleFor(x => x.VatRate)
            .GreaterThan(0).WithMessage("VAT rate must be greater than 0.")
            .Must(BeAValidAustrianVatRate).WithMessage("VAT rate must be a valid Austrian rate (10%, 13%, or 20%).");

        RuleFor(x => x)
            .Must(x => new[] { x.NetAmount, x.GrossAmount, x.VatAmount }.Count(v => v.HasValue) == 1)
            .WithMessage("Exactly one amount (Net, Gross, or VAT) must be provided.");

        When(x => x.NetAmount.HasValue, () =>
        {
            RuleFor(x => x.NetAmount)
                .GreaterThan(0).WithMessage("Net amount must be greater than 0.");
        });

        When(x => x.GrossAmount.HasValue, () =>
        {
            RuleFor(x => x.GrossAmount)
                .GreaterThan(0).WithMessage("Gross amount must be greater than 0.");
        });

        When(x => x.VatAmount.HasValue, () =>
        {
            RuleFor(x => x.VatAmount)
                .GreaterThan(0).WithMessage("VAT amount must be greater than 0.");
        });
    }
    
    private static bool BeAValidAustrianVatRate(decimal vatRate)
    {
        var validVatRates = new[] { 10m, 13m, 20m };
        return validVatRates.Contains(vatRate);
    }
}