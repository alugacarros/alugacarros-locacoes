using AlugaCarros.Core.WebApi;
using AlugaCarros.Locacoes.Functions.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AlugaCarros.Locacoes.Functions;
public class CompatibilizarStatusCarroLocadoFunction
{
    private readonly IVehicleService _vehicleService;
    public CompatibilizarStatusCarroLocadoFunction(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    private const string queueTrigger = "compatibilizar-status-carro-locado";

    [FunctionName(nameof(CompatibilizarStatusCarroLocadoFunction))]
    public async Task Run([ServiceBusTrigger(queueTrigger, Connection = "ServiceBusConnectionString")] string message, ILogger log)
    {
        log.LogInformation($"[Function={queueTrigger}] [Message={message}]");
        var messageModel = message.Deserialize<CompatibilizarStatusCarroLocadoModel>();

        if (messageModel == null)
        {
            var errorMessage = $"Error while deserialize the object {message} to {typeof(CompatibilizarStatusCarroLocadoModel)}";
            log.LogError(errorMessage);
            throw new Exception(errorMessage);
        }

        await _vehicleService.RentCar(messageModel.VehiclePlate);
    }

    private class CompatibilizarStatusCarroLocadoModel
    {
        public string VehiclePlate { get; set; }
    }
}
