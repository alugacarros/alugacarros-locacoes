using AlugaCarros.Core.WebApi;
using AlugaCarros.Locacoes.Functions.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AlugaCarros.Locacoes.Functions.Services;

public class AuthService : IAuthService
{
    private HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("Auth"); 
        _configuration = configuration;
    }

    public async Task<string> GetToken()
    {
        var content = new
        {
            email = _configuration["FunctionAuthLogin"],
            password = _configuration["FunctionAuthPassword"]
        };
        var loginResponse = await _httpClient.PostAsync("api/v1/users/login", content.ToStringContent());
        var login = await loginResponse.Deserialize<LoginResponse>();
        return login.AccessToken;
    }

    private class LoginResponse
    {
        public string AccessToken { get; set; }
    }
}
