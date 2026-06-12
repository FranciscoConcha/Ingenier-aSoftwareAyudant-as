namespace ProyectoDivine.Src.Dtos.Payment;

public class ProcessPaymentData
{
    //Datos para la reserva
    
    public string ReservationCode{get;set;} = string.Empty;
    /// Si se procesa directamente el págo al crear la reserva se puede agregar aquí pero no es necesario
    public int? Amount {get;set;}
    // Datos de la tarjeta
    public string CardNum{get;set;} =string.Empty;
    public string SecurityCode{get;set;} =string.Empty;
    public string TableExpiration {get;set;} = string.Empty;
    public string AgeExpiration {get;set;} = string.Empty;
    public string NameHolder {get;set;} =string.Empty;

}
public class DataResponseProcessPaymen
{
    public int MovementCod {get;set;}
    public string ReservationCode {get;set;} =string.Empty;
    public int AmountCharged {get;set;}

}
public class ResponseProcessPayment
{
    public string Message { get; set; } = null!;
    public bool Success { get; set; }
    public DataResponseProcessPaymen? Data { get; set; } 
}
