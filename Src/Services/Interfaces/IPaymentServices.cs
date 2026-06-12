using ProyectoDivine.Src.Dtos.Payment;

namespace ProyectoDivine.Src.Services.interfaces;

public interface IPaymentServices
{
    Task<ResponseProcessPayment> ProcessPayment(int userId, ProcessPaymentData data);
}