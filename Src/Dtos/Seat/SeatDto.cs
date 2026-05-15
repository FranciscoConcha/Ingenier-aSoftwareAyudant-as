namespace ProyectoDivine.Src.Dtos.Seat;
public class SeatDto
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = null!;
    public string Section { get; set; } = null!;
    public decimal Price { get; set; }
    public int Status { get; set; }
}
public class GetSeatsResponse
{
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    public List<SeatDto> Data { get; set; } = [];
}