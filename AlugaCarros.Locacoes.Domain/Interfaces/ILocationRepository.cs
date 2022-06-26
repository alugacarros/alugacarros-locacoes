using AlugaCarros.Locacoes.Domain.Entities;

namespace AlugaCarros.Locacoes.Domain.Interfaces;
public interface ILocationRepository : IRepository
{
    public Task AddLocation(Location location);
    Task<List<Location>> FindLocationsByAgencyCode(string agencyCode);
}
