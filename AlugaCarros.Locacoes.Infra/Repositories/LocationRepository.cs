using AlugaCarros.Locacoes.Domain.Entities;
using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Infra.Data;

namespace AlugaCarros.Locacoes.Infra.Repositories;
public class LocationRepository : ILocationRepository
{
    private readonly LocacoesDbContext _dbContext;

    public LocationRepository(LocacoesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddLocation(Location location)
    {
        await _dbContext.Locations.AddAsync(location);
    }

    public IUnitOfWork UnitOfWork => _dbContext;    
}
