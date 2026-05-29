namespace ProyectoDivine.Src.Dtos.Reservation;
/// <summary>
/// Dto para mostrar los datos de una reserva asociada y generar el pdf
/// </summary>
public class PdfReservationDto
{
    public int Id { get; set; }
    public string ReservationCode { get; set; } = null!;
    public int FunctionId { get; set; }
    public string FuncionTitle { get; set; } = null!;
    public List<ReservationSeatDto> SelectedSeats { get; set; } = [];
    public int Status { get; set; }
    public DateTime FunctionDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }

}
/// <summary>
/// Dto de respuesta para mostrar el mensaje de éxito o error al generar el pdf de la reserva, 
/// junto con el archivo PDF en formato de bytes.
/// </summary>
public class PdfReservationResponse
{
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    public byte[] Data { get; set; } = [];
}