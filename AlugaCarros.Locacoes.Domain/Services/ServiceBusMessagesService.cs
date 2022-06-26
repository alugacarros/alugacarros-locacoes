using AlugaCarros.Locacoes.Domain.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace AlugaCarros.Locacoes.Domain.Services;

public class ServiceBusMessagesService : IServiceBusMessagesService
{
    private readonly ILogger<ServiceBusMessagesService> _logger;

    private readonly ServiceBusClient _serviceBusClient;
    public ServiceBusMessagesService(ServiceBusClient serviceBusClient, ILogger<ServiceBusMessagesService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    public async Task Publish(string queue, object message)
    {
        ServiceBusSender? sender = null;
        try
        {
            var stringMessage = System.Text.Json.JsonSerializer.Serialize(message);

            sender = _serviceBusClient.CreateSender(queue);
            
            await sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(stringMessage)));

            _logger.LogInformation($"[Mensagem enviada] [Fila={queue}] [Message={stringMessage}]");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exceção: {ex.GetType().FullName} | Mensagem: {ex.Message}");
            throw;
        }
        finally
        {
            if (sender is not null)
            {
                await sender.CloseAsync();
                _logger.LogInformation("Conexao do Sender fechada!");
            }
        }
    }

}
