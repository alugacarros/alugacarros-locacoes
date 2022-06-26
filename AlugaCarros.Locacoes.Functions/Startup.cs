using AlugaCarros.Core.WebApi;
using AlugaCarros.Locacoes.Functions.Extensions;
using AlugaCarros.Locacoes.Functions.Services;
using AlugaCarros.Locacoes.Functions.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AlugaCarros.Locacoes.Functions.Startup))]
namespace AlugaCarros.Locacoes.Functions;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        string text = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>()["ApplicationInsights:InstrumentationKey"];
        if (!string.IsNullOrEmpty(text))
        {
            builder.Services.AddApplicationInsightsTelemetry(text);
        }

        var authUrl = builder.GetService<IConfiguration>()["AuthUrl"];
        var reservationsUrl = builder.GetService<IConfiguration>()["ReservationsUrl"];
        var vehicleUrl = builder.GetService<IConfiguration>()["VehiclesUrl"];
        builder.Services.AddHttpClient("Auth", authUrl);
        builder.Services.AddHttpClient("Reservation", reservationsUrl);
        builder.Services.AddHttpClient("Vehicle", vehicleUrl);
        
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

    }
}