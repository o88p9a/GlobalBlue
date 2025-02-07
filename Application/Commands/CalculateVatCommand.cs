using MediatR;

namespace Application.Commands;

public class CalculateVatCommand : IRequest<VatCalculationResponse>
{
    public decimal? NetAmount { get; set; }
    public decimal? GrossAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal VatRate { get; set; }
}