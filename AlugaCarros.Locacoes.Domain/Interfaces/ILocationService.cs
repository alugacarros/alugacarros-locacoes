using AlugaCarros.BalcaoAtendimento.Core.ServiceResponse;
using AlugaCarros.Locacoes.Domain.RequestResponse;

namespace AlugaCarros.Locacoes.Domain.Interfaces;

public interface ILocationService
{
    Task<ServiceResponse<string>> CreateLocation(CreateLocationRequest createLocationRequest);
}