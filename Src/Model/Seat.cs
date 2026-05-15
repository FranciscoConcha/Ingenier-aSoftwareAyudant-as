namespace ProyectoDivine.Src.Model;
public class Seat
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = null!;
    public string Section { get; set; } = null!;
    public decimal Price { get; set; }
    public int Status { get; set; } = 0;
    public int FuntionId { get; set; }
    public Funtion Funtion { get; set; } = null!;
}