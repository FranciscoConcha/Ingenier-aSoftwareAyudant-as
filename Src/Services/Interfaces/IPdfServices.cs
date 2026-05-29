using ProyectoDivine.Src.Dtos.Reservation;

namespace ProyectoDivine.Src.Services.interfaces;

public interface IPdfServices
{
    /// <summary>
    /// Genera un PDF a partir de los datos de una reserva.
    /// </summary>
    /// <param name="reservationData">
    /// DTO que contiene los datos de la reserva para generar el PDF.
    /// </param>
    /// <returns>
    /// arreglo de bytes que contiene el PDF generado.
    /// </returns>
    Task<PdfReservationResponse> GeneratePdfAsync(PdfReservationDto reservationData);
}