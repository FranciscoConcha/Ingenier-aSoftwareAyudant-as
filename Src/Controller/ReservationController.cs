namespace ProyectoDivine.Src.Controller;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ProyectoDivine.Src.Dtos.Reservation;
using ProyectoDivine.Src.Services.interfaces;
[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationServices reservationServices): ControllerBase
{
    private readonly IReservationServices _reservationServices = reservationServices;

    [HttpPost()]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservation request)
    {
        try
        {
            //Nos permite obtener el nombre de usuario desde el token
            var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;    
            // Si no se encuentra el claim o no es un número válido, devolvemos un error de autenticación
            if(!int.TryParse(userClaimId,out int userId))
            {
                return Unauthorized(new CreateReservationResponse
                {
                    Success = false,
                    Message = "Usuario no autenticado.",
                    Data = null!
                });
            }
            var response = await _reservationServices.CreateReservationAsync(userId, request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            // Devolvemos un Created con la ubicación de la nueva reserva utilizando el código de reserva generado
            // La ubicación se construye como "/api/reservation/{reservationCode}", donde reservationCode es el código único de la reserva creada
            // Esto permite que el cliente pueda acceder a la nueva reserva utilizando la URL proporcionada en la respuesta
            return Created($"/api/reservation/{response.Data?.ReservationCode}", response);
        }catch(Exception ex)
        {
            return StatusCode(500, new CreateReservationResponse
            {
                Success = false,
                Message = $"Error al crear la reserva: {ex.Message}",
                Data = null!
            });
        }
    }

}