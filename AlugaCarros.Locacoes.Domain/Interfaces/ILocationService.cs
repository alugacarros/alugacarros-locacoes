using AlugaCarros.Core.Dtos;
using AlugaCarros.Locacoes.Domain.RequestResponse;

namespace AlugaCarros.Locacoes.Domain.Interfaces;

public interface ILocationService
{
    Task<ResultDto<string>> CreateLocation(CreateLocationRequest createLocationRequest);
    Task<List<LocationResponse>> GetLocations(string agencyCode);
}