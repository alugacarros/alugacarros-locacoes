using AlugaCarros.Locacoes.Api.Configuration;
using AlugaCarros.Locacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiConfiguration(builder.Configuration);

var app = builder.Build();

app.UseAppConfiguration();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<LocacoesDbContext>();
    dataContext.Database.Migrate();
}

app.Run();
