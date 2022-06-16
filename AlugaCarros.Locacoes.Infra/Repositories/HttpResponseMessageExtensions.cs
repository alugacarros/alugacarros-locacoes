using System.Text.Json;

namespace AlugaCarros.Locacoes.Infra.Repositories;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> Deserialize<T>(this HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await responseMessage?.Content?.ReadAsStringAsync(), options);
    }
}
