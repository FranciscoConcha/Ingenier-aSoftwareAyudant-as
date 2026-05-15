using System.Data;
using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Db;
using ProyectoDivine.Src.Dtos.Reservation;
using ProyectoDivine.Src.Dtos.Seat;
using ProyectoDivine.Src.Model;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Services;

public class ReservationServices(ContextDb contextDb) : IReservationServices
{
    private readonly ContextDb _contextDb = contextDb;
    public Task<CreateReservationResponse> CancelReservationAsync(string reservationCode, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateReservationResponse> CreateReservationAsync(int userId, CreateReservation request)
    {
        if (request.SeatIds == null || request.SeatIds.Count == 0)
        {
            return new CreateReservationResponse
            {
                Success = false,
                Message = "Debe seleccionar al menos un asiento.",
                Data = null!
            };
        }
        if(request.FuntionId <= 0)
        {
            return new CreateReservationResponse
            {
                Success = false,
                Message = "ID de función no válido.",
                Data = null!
            };
        }
        try
        {
            var funtion = await _contextDb.Functions.FirstOrDefaultAsync(f =>f.Id==request.FuntionId);
            if(funtion == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    Message = "Función no encontrada.",
                    Data = null!
                };
            }
            //Esto es hacer Select * from seat where id in (lista de ids) and funtionsid = id de la función
            var seatsSelected = await _contextDb.Seats
                .Where(s => request.SeatIds.Contains(s.Id) && 
                    s.FuntionId == request.FuntionId).ToListAsync(); 
            if(seatsSelected.Count != request.SeatIds.Count)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    Message = "Algunos asientos no fueron encontrados para la función seleccionada.",
                    Data = null!
                };
            }

            using(var transaction = await _contextDb.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    var unavailableSeats = seatsSelected.Where(s=>s.Status != 0).ToList();
                    if (unavailableSeats.Count != 0)
                    {
                        var SeatNumbers = string.Join(", ", unavailableSeats.Select(s => s.SeatNumber));
                        await transaction.RollbackAsync();
                        return new CreateReservationResponse
                        {
                            Success = false,
                            Message = $"Los siguientes asientos no están disponibles: {SeatNumbers}",
                            Data = null!
                        };

                    }
                    // Genera una id de reserva única y compacta para el código de reserva gracias al formato "N" se genera sin guiones y con mayúsculas
                    //Al final es un UUID con mayusculas y sin guiones.
                    var reservationCode = Guid.NewGuid().ToString("N")[..12].ToUpper();
                    var totalPrice = seatsSelected.Sum(s => s.Price);

                    var reservation = new Reservation
                    {
                        ReservationCode = reservationCode,
                        UserId = userId,
                        FuntionId = request.FuntionId,
                        SelectedSeats = seatsSelected,
                        TotalPrice = totalPrice,
                        Status = 0, // 0 = pendiente, 1 = confirmada
                        CreatedAt = DateTime.UtcNow
                    };
                    foreach(var seat in seatsSelected)
                    {
                        seat.Status = 1; // Marcar como reservado
                    }

                    await _contextDb.Reservations.AddAsync(reservation);
                    await _contextDb.SaveChangesAsync();
                    await transaction.CommitAsync();
                    var reservationDto =new ReservationData
                    {
                        Id = reservation.Id,
                        ReservationCode = reservation.ReservationCode,
                        FuntionId = reservation.FuntionId,
                        MovieTitle = funtion.Name,
                        SelectedSeats = [.. seatsSelected.Select(s => new ReservationSeatDto
                        {
                            Id = s.Id,
                            SeatNumber = s.SeatNumber,
                            Section = s.Section,
                            Price = s.Price
                        })],
                        TotalPrice = reservation.TotalPrice,
                        Status = reservation.Status,
                        CreatedAt = reservation.CreatedAt
                    };
                    return new CreateReservationResponse
                    {
                        Success = true,
                        Message = "Reserva creada exitosamente.",
                        Data = reservationDto
                    };
                }catch(DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    return new CreateReservationResponse
                    {
                        Success = false,
                        Message = $"Error al crear la reserva: {ex.Message}",
                        Data = null!
                    };
                }
            }
        }
        catch (Exception ex)
        {
            return new CreateReservationResponse
            {
                Success = false,
                Message = $"Error inesperado: {ex.Message}",
                Data = null!
            };
        }

        throw new NotImplementedException();
    }

    public Task<GetSeatsResponse> GetFuntionSeatsAsync(int funtionId)
    {
        throw new NotImplementedException();
    }

    public Task<MyReservationsResponse> GetMyReservationsAsync(int userId)
    {
        throw new NotImplementedException();
    }
}