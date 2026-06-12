using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDivine.Src.Dtos.Payment;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Controller;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IPaymentServices paymentServices) : ControllerBase
{
    private readonly IPaymentServices _paymentServices = paymentServices;

    [HttpPut("Pagar")]
    [Authorize]
    public async Task<ActionResult<ResponseProcessPayment>> PaymentReservation([FromBody] ProcessPaymentData data)
    {
        try
        {
            var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userClaimId,out int userId))
            {
                return Unauthorized(new ResponseProcessPayment
                {
                    Success = false,
                    Message = "Usuario no autenticado.",
                    Data = null!
                });
            }
            var response = await _paymentServices.ProcessPayment(userId,data);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }
        catch(Exception err)
        {
            return StatusCode(500,new ResponseProcessPayment
            {
                Message = "Error en el servidor " +err.Message,
                Success=false
            });
        }
    }
}
