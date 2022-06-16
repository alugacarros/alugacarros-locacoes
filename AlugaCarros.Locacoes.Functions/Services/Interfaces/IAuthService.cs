namespace AlugaCarros.Locacoes.Functions.Services.Interfaces;
public interface IAuthService
{
    Task<string> GetToken();
}
