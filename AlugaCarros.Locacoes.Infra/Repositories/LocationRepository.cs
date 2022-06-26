using AlugaCarros.Locacoes.Domain.Entities;
using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Location>> FindLocationsByAgencyCode(string agencyCode)
        => await _dbContext.Locations.Where(w => w.AgencyCode == agencyCode).ToListAsync();

    public IUnitOfWork UnitOfWork => _dbContext;
}
