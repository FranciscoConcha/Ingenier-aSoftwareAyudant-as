namespace ProyectoDivine.Src.Model;

public class Payment{
    public int Id {get;set;}
    public int Amount {get;set;}
    public DateTime DateTransaccion {get;set;}
    /// <summary>
    /// Estados{
    ///     0:No realizado,
    ///     1:Realizado,
    ///     2:Cancelado    
    /// }
    /// 
    /// </summary>
    public int Status {get;set;}
    //-1 Cuando no se realizo
    public int PaymentApiId {get;set;}
    public int UserId{get;set;}
    public User User {get;set;} =null!;
    public int ReservationId {get;set;}
    public Reservation Reservation {get;set;} = null!;

}