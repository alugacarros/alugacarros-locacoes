using AlugaCarros.Core.WebApi;
using AlugaCarros.Locacoes.Functions.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AlugaCarros.Locacoes.Functions
{
    public class CompatibilizarReservaRealizadaFunction
    {      
        private readonly IReservationService _reservationService;
        public CompatibilizarReservaRealizadaFunction(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        private const string queueTrigger = "compatibilizar-reservas-realizada";

        [FunctionName(nameof(CompatibilizarReservaRealizadaFunction))]
        public async Task Run([ServiceBusTrigger(queueTrigger, Connection = "ServiceBusConnectionString")]string message, ILogger log)
        {                        
            log.LogInformation($"[Function={queueTrigger}] [Message={message}]");
            var messageModel = message.Deserialize<CompatibilizarReservasRealizadasModel>();

            if(messageModel == null)
            {
                var errorMessage = $"Error while deserialize the object {message} to {typeof(CompatibilizarReservasRealizadasModel)}";
                log.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            await _reservationService.CloseReservation(messageModel.ReservationCode);
        }

        private class CompatibilizarReservasRealizadasModel
        {
            public string ReservationCode { get; set; }
        }
    }
}
