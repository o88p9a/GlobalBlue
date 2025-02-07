using Application.Commands;
using GlobalBlue.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue.Controllers;

[ApiController]
[Route("api/vat")]
public class VatController(IMediator mediator) : ControllerBase
{
    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] VatCalculationRequest request)
    {
        var result = await mediator.Send(new CalculateVatCommand
        {
            GrossAmount = request.GrossAmount,
            VatAmount = request.VatAmount,
            VatRate = request.VatRate,
            NetAmount = request.NetAmount
        });
        return Ok(result);
    }
}