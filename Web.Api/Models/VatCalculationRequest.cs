namespace GlobalBlue.Models;

public class VatCalculationRequest
{
    public decimal? NetAmount { get; set; }
    public decimal? GrossAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public decimal VatRate { get; set; }
}