using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProyectoDivine.Src.Db;
using ProyectoDivine.Src.Dtos.Payment;
using ProyectoDivine.Src.Model;
using ProyectoDivine.Src.Services.interfaces;

namespace ProyectoDivine.Src.Services;

public class PaymentServices(ContextDb contextDb, IConfiguration config, IHttpClientFactory httpClientFactory) : IPaymentServices
{
    private readonly ContextDb _contextDb = contextDb;
    private readonly IConfiguration _config = config;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    private readonly string BaseUrl = config["ApiPayment:BaseUrl"] ?? "";
    private readonly string StatusApi = config["ApiPayment:HealthMetod"]??"";
    private readonly string ProcessPaymentApi = config["ApiPayment:ProcessMetod"]?? "";
    
    public async Task<ResponseProcessPayment> ProcessPayment(int userId, ProcessPaymentData data)
    {
        try
        {
            // Validamos si se enviaron numeros o algo difernte
            if (!long.TryParse(data.CardNum, out _))
            {
                return new ResponseProcessPayment
                {
                    Message = "Número de tarjeta no númerico",
                    Success = false
                };
            }
            //Validación de la tarjeta ↓
            if(!int.TryParse(data.AgeExpiration, out _))
            {
                return new ResponseProcessPayment
                {
                    Message = "Fecha no valida",
                    Success = false
                };
            }
            var ageTarjet = "20"+data.AgeExpiration;

            //Validaciones faltantes ↓
            var response = await _contextDb.Reservations.FirstOrDefaultAsync(r =>r.ReservationCode ==data.ReservationCode);
            if(response == null)
            {
                return new ResponseProcessPayment
                {
                    Message ="Reserva no existente",
                    Success = false
                };
            }

            var userResponse = await _contextDb.Users.FirstOrDefaultAsync(u =>u.Id == userId);
            if(userResponse == null)
            {
                return new ResponseProcessPayment {
                    Message= "Usuario no existe",
                    Success =false
                };
            }

            var existPayment = await _contextDb.Payments
                                            .Include(p =>p.Reservation)
                                            .Include(p=>p.User)
                                            .FirstOrDefaultAsync(r=>r.Reservation.ReservationCode == data.ReservationCode 
                                                && r.User.Id == userId);
            if(existPayment == null)
            {
                return new ResponseProcessPayment
                {
                    Message ="Proceso de pago inexistente",
                    Success = false
                };
            }
            // Consumir api de la tarjeta
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(30);

            var responseApi = await client.GetAsync($"{BaseUrl}{StatusApi}");
            if (!responseApi.IsSuccessStatusCode  )
            {
                return new ResponseProcessPayment
                {
                    Success = false,
                    Message = "Error con la API externa"
                };
            }
            var amountRequest =data.Amount;
            if(data.Amount == null || data.Amount == 0)
            {
                amountRequest = existPayment.Reservation.TotalPrice;
            }
            var request =new 
            {
                numero_tarjeta=data.CardNum,
                codigo_seguridad=data.SecurityCode,
                mes_expiracion=data.TableExpiration,
                anio_expiracion=ageTarjet,
                nombre_titular=data.NameHolder,
                monto= amountRequest ,
                codigo_reserva= data.ReservationCode,
                email_cliente=userResponse.Email
            };
            var apiResponsePayment = await client.PostAsJsonAsync(
                $"{BaseUrl}{ProcessPaymentApi}",
                request
            );
            if (!apiResponsePayment.IsSuccessStatusCode)
            {
                return new ResponseProcessPayment {
                    Message ="Error al procesar con la api externa",
                    Success =false  
                };
            }
            var content = await apiResponsePayment.Content.ReadAsByteArrayAsync();
            var statusResponseContent = JsonSerializer.Deserialize<DataResponseProcessPaymen>(content);
            if(statusResponseContent == null)
            {
                return new ResponseProcessPayment{
                    Message ="Datos no envias por api externa",
                    Success = false
                };
            }
            existPayment.Status = 1;
            existPayment.DateTransaccion = DateTime.UtcNow;
            existPayment.PaymentApiId = statusResponseContent.MovementCod;
            existPayment.Amount = (int)statusResponseContent.AmountCharged;
            await _contextDb.SaveChangesAsync();
            return new ResponseProcessPayment
            {
                Message="Se realizo el proceso de compra",
                Success =true,
                Data = statusResponseContent
            };

            
        }
        catch(Exception err)
        {
            return new ResponseProcessPayment
            {
                Message ="Error en el metodo " +err.Message,
                Success = false
            };
        }
        throw new NotImplementedException();
    }
    
}