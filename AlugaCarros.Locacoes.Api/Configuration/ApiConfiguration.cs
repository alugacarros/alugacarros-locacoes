using AlugaCarros.BalcaoAtendimento.Core.ApiConfiguration;
using AlugaCarros.Core.WebApi;
using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Domain.Services;
using AlugaCarros.Locacoes.Infra.Data;
using AlugaCarros.Locacoes.Infra.Repositories;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;

namespace AlugaCarros.Locacoes.Api.Configuration;
public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultApiConfiguration(configuration);

        AddDatabaseMySql(services, configuration);
        
        services.AddAuthentication(configuration);

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<HttpClientAuthorizationDelegatingHandler>("Vehicles", configuration["VehiclesUrl"]);        
        services.AddHttpClient<HttpClientAuthorizationDelegatingHandler>("Reservations", configuration["ReservationsUrl"]);
        
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<ILocationRepository, LocationRepository>();
        services.AddTransient<IVehicleApiRepository, VehicleApiRepository>();
        services.AddTransient<IReservationApiRepository, ReservationApiRepository>();
        services.AddSingleton<IServiceBusMessagesService, ServiceBusMessagesService>();

        services.AddSingleton(sp => new ServiceBusClient(configuration["ServiceBusConnectionString"]));

        return services;
    }

    private static void AddDatabaseMySql(IServiceCollection services, IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
        var connectionString = $"Server={configuration["MySqlHost"]};Port=3306;Database=alugacarros-locacoes;Uid={configuration["MySqlUser"]};Pwd={configuration["MySqlPass"]};"
         ?? "Server=127.0.0.1;Port=3306;Database=alugacarros-locacoes;Uid=alugacarros;Pwd=alugacarros;";

        services.AddDbContext<LocacoesDbContext>(options =>
        options.UseMySql(connectionString, serverVersion));
    }
    public static WebApplication UseAppConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseCors("Total");

        return app;
    }
}
