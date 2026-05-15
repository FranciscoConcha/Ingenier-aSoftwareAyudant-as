namespace ProyectoDivine.Src.Model;
public class Reservation
{
    public int Id { get; set; }
    public string ReservationCode { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int FuntionId { get; set; }
    public Funtion Funtion { get; set; } = null!;
    public List<Seat> SelectedSeats { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public int Status { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
}