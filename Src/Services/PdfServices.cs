using QuestPDF.Fluent;
using ProyectoDivine.Src.Dtos.Reservation;
using ProyectoDivine.Src.Services.interfaces;
using QuestPDF.Helpers;

namespace ProyectoDivine.Src.Services;

public class PdfServices : IPdfServices
{

    public async Task<PdfReservationResponse> GeneratePdfAsync(PdfReservationDto reservationData)
    {
        try
        {            
            var pdf = await Task.Run(() =>
            {
                return Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Roboto"));

                        page.Content().Column(column =>
                        {
                            // HEADER
                            column.Item()
                                .AlignCenter()
                                .PaddingBottom(10)
                                .Text("Reserva de Teatro Divine 🎭")
                                .FontSize(20)
                                .Bold()
                                .FontColor("#62625e");

                            column.Item()
                                .PaddingBottom(5)
                                .LineHorizontal(5)
                                .LineColor("#62625e");

                            // TÍTULO
                            column.Item()
                                .PaddingBottom(5)
                                .Text($"Entrada del teatro para la obra: {reservationData.FuncionTitle}")
                                .FontSize(18)
                                .Bold()
                                .FontColor("");

                            // FECHA Y HORA + SALA
                            column.Item()
                                .PaddingVertical(10)
                                .Row(row =>
                                {
                                    row.RelativeItem()
                                        .Background("#f5f5f5")
                                        .Padding(12)
                                        .Border(1)
                                        .BorderColor("#ddd")
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("Fecha y hora:")
                                                .FontSize(14)
                                                .Bold()
                                                .FontColor("#1a1a2e");
                                            
                                            col.Item()
                                                .PaddingTop(9)
                                                .Text($"{reservationData.FunctionDate:dddd, dd 'de' MMMM 'de' yyyy, HH:mm}")
                                                .FontSize(13)
                                                .Bold()
                                                .FontColor("#1a1a2e");
                                        });

                                    row.RelativeItem().PaddingHorizontal(5);

                                    row.RelativeItem()
                                        .Background("#f5f5f5")
                                        .Padding(12)
                                        .Border(1)
                                        .BorderColor("#ddd")
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("Sala:")
                                                .FontSize(14)
                                                .Bold()
                                                .FontColor("#1a1a2e");
                                            
                                            col.Item()
                                                .PaddingTop(9)
                                                .Text("Sala principal")
                                                .FontSize(13)
                                                .Bold()
                                                .FontColor("#1a1a2e");
                                        });
                                });

                            // ASIENTOS
                            column.Item()
                                .PaddingBottom(15)
                                .Column(seatColumn =>
                                {
                                    seatColumn.Item()
                                        .Text("Asientos reservados:")
                                        .FontSize(9)
                                        .Bold()
                                        .FontColor("#1a1a2e");

                                    seatColumn.Item()
                                        .PaddingTop(8)
                                        .Row(row =>
                                        {
                                            foreach (var seat in reservationData.SelectedSeats)
                                            {
                                                row.RelativeItem()
                                                    .Border(1)
                                                    .BorderColor("#ef7021")
                                                    .Background("#fffef0")
                                                    .Padding(8)
                                                    .Column(col =>
                                                    {
                                                        col.Item()
                                                            .Text($"Número de asiento: {seat.SeatNumber}")
                                                            .FontSize(12)
                                                            .Bold()
                                                            .AlignCenter()
                                                            .FontColor("#1a1a2e");
                                                        
                                                        col.Item()
                                                            .Text($"Sección: {seat.Section}")
                                                            .AlignCenter()
                                                            .FontSize(11)
                                                            .FontColor("#1a1a2e");
                                                        
                                                        col.Item()
                                                            .Text($"Precio: ${seat.Price:N0}")
                                                            .AlignCenter()
                                                            .FontSize(8)
                                                            .FontColor("#1a1a2e");
                                                    });
                                            }
                                        });
                                });

                            // TITULAR Y CÓDIGO
                            column.Item()
                                .PaddingVertical(10)
                                .Background("#f5f5f5")
                                .Padding(12)
                                .Border(1)
                                .BorderColor("#ddd")
                                .Row(row =>
                                {
                                    row.RelativeItem()
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("Titular")
                                                .FontSize(9)
                                                .Bold()
                                                .FontColor("#666");

                                            col.Item()
                                                .PaddingTop(8)
                                                .Text("Usuario Registrado")
                                                .FontSize(12)
                                                .FontColor("#1a1a2e");
                                        });

                                    row.RelativeItem(2)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("Código de reserva")
                                                .FontSize(9)
                                                .Bold()
                                                .FontColor("#666");

                                            col.Item()
                                                .PaddingTop(8)
                                                .Text($"{reservationData.ReservationCode}")
                                                .FontSize(12)
                                                .FontColor("#1a1a2e");
                                        });
                                });

                            // CÓDIGO DE ENTRADA PRINCIPAL
                            column.Item()
                                .PaddingTop(15)
                                .Background("#ef7021")
                                .Padding(12)
                                .AlignCenter()
                                .Column(col =>
                                {
                                    col.Item()
                                        .Text("Código de entrada")
                                        .FontSize(14)
                                        .Bold()
                                        .FontColor("#fffef0");

                                    col.Item()
                                        .PaddingTop(10)
                                        .Text(reservationData.ReservationCode)
                                        .FontSize(16)
                                        .Bold()
                                        .FontColor("#fffef0");
                                });

                            // TOTAL A PAGAR
                            column.Item()
                                .PaddingTop(15)
                                .Row(row =>
                                {
                                    row.RelativeItem()
                                        .Background("#1a1a2e")
                                        .Padding(12)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("TOTAL A PAGAR")
                                                .FontSize(10)
                                                .Bold()
                                                .FontColor("#ef7021");
                                            
                                            col.Item()
                                                .PaddingTop(8)
                                                .Text($"${reservationData.TotalPrice:N0}")
                                                .FontSize(18)
                                                .Bold()
                                                .FontColor("#ef7021");
                                        });
                                });

                            // FOOTER
                            column.Item()
                                .PaddingTop(10)
                                .LineHorizontal(1)
                                .LineColor("#ddd");

                            column.Item()
                                .PaddingTop(10)
                                .AlignCenter()
                                .Column(col =>
                                {
                                    col.Item()
                                        .Text("¡Gracias por tu compra! 🎟️")
                                        .FontSize(14)
                                        .Bold()
                                        .FontColor("#1a1a2e");
                                    
                                    col.Item()
                                        .PaddingTop(5)
                                        .Text("Presenta este código de entrada en la taquilla del teatro el día de la función.")
                                        .FontSize(10)
                                        .FontColor("#666")
                                        .AlignCenter();
                                    
                                    col.Item()
                                        .PaddingTop(8)
                                        .Text($"Descargado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                                        .FontSize(8)
                                        .FontColor("#999");
                                });
                        });
                    });
                })
                .GeneratePdf();
            });


            return new PdfReservationResponse
            {
                Success = true,
                Message = "PDF generado exitosamente",
                Data = pdf
            };
        }
        catch (Exception ex){


            return new PdfReservationResponse
            {
                Success = false,
                Message = $"Error al generar el PDF: {ex.Message}",
                Data = []
            };
        }
    }
}