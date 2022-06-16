namespace AlugaCarros.Locacoes.Domain.Interfaces;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}