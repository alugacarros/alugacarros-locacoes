using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlugaCarros.Locacoes.Domain.Services;

public abstract class ServiceBusWorker : IHostedService
{
    private readonly ServiceBusProcessor _processor;
    protected readonly ILogger<ServiceBusWorker> _logger;

    public ServiceBusWorker(ServiceBusClient serviceBusClient, ILogger<ServiceBusWorker> logger, string queueName)
    {
        _processor = serviceBusClient.CreateProcessor(queueName);
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _processor.StopProcessingAsync();
        await _processor.DisposeAsync();
    }

    protected abstract Task MessageHandler(ProcessMessageEventArgs args);

    protected abstract Task ErrorHandler(ProcessErrorEventArgs args);
}