namespace ProyectoDivine.Src.Dtos.Reservation;
public class MyReservationDto
{
    public string ReservationCode { get; set; } = null!;
    public string MovieTitle { get; set; } = null!;
    public DateTime FuntionDateTime { get; set; }
    public int SeatCount { get; set; }
    public decimal TotalPrice { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class MyReservationsResponse
{
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    public List<MyReservationDto> Data { get; set; } = [];
}
