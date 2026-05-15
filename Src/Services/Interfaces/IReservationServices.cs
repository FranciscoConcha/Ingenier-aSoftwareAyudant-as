using ProyectoDivine.Src.Dtos.Reservation;
using ProyectoDivine.Src.Dtos.Seat;

namespace ProyectoDivine.Src.Services.interfaces;
public interface IReservationServices
{
    Task<GetSeatsResponse> GetFuntionSeatsAsync(int funtionId);
    Task<CreateReservationResponse> CreateReservationAsync(int userId, CreateReservation request);
    Task<MyReservationsResponse> GetMyReservationsAsync(int userId);
    Task<CreateReservationResponse> CancelReservationAsync(string reservationCode, int userId);
}