namespace ProyectoDivine.Src.Dtos.Reservation;
public class CreateReservation
{
    public int FuntionId { get; set; }
    public List<int> SeatIds { get; set; } = [];
}
public class ReservationSeatDto
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = null!;
    public string Section { get; set; } = null!;
    public decimal Price { get; set; }
}
public class ReservationData
{
    public int Id { get; set; }
    public string ReservationCode { get; set; } = null!;
    public int FuntionId { get; set; }
    public string MovieTitle { get; set; } = null!;
    public List<ReservationSeatDto> SelectedSeats { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class CreateReservationResponse
{
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    public ReservationData Data { get; set; } = null!;
}
