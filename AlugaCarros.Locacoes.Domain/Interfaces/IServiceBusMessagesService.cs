namespace AlugaCarros.Locacoes.Domain.Interfaces;

public interface IServiceBusMessagesService
{
    Task Publish(string queue, object message);
}