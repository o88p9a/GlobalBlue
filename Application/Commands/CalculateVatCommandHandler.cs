using MediatR;

namespace Application.Commands;

public class CalculateVatCommandHandler : IRequestHandler<CalculateVatCommand, VatCalculationResponse>
{
    public Task<VatCalculationResponse> Handle(CalculateVatCommand request, CancellationToken cancellationToken)
    {
        decimal netAmount = 0;
        decimal grossAmount = 0;
        decimal vatAmount = 0;

        if (request.NetAmount.HasValue)
        {
            netAmount = request.NetAmount.Value;
            grossAmount = netAmount * (1 + request.VatRate / 100);
            vatAmount = grossAmount - netAmount;
        }
        else if (request.GrossAmount.HasValue)
        {
            grossAmount = request.GrossAmount.Value;
            netAmount = grossAmount / (1 + request.VatRate / 100);
            vatAmount = grossAmount - netAmount;
        }
        else if (request.VatAmount.HasValue)
        {
            vatAmount = request.VatAmount.Value;
            netAmount = vatAmount / (request.VatRate / 100);
            grossAmount = netAmount + vatAmount;
        }

        return Task.FromResult(new VatCalculationResponse
        {
            NetAmount = netAmount,
            GrossAmount = grossAmount,
            VatAmount = vatAmount
        });
    }
}