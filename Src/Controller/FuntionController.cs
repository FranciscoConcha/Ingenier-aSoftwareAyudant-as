using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDivine.Src.Dtos.Funtion;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Controller;
[ApiController]
[Route("api/[controller]")]
public class FuntionController(IFuntionServices funtionServices): ControllerBase
{
    private readonly IFuntionServices _funtionServices = funtionServices;

    [HttpPost("")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateFuntion([FromForm] CreateFuntion request)
    {
        try
        {
            var response = await _funtionServices.CreateFuntionAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new CreateFuntionResponse
            {
                Success = false,
                Message = $"Error al crear la función: {ex.Message}",
                Data = null!
            });
        }
    }
}