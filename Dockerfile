FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR .
COPY ["AlugaCarros.Locacoes.Api/AlugaCarros.Locacoes.Api.csproj", "./AlugaCarros.Locacoes.Api/"]
COPY ["AlugaCarros.Locacoes.Domain/AlugaCarros.Locacoes.Domain.csproj", "./AlugaCarros.Locacoes.Domain/"]
COPY ["AlugaCarros.Locacoes.Infra/AlugaCarros.Locacoes.Infra.csproj", "./AlugaCarros.Locacoes.Infra/"]

RUN dotnet restore "AlugaCarros.Locacoes.Api/AlugaCarros.Locacoes.Api.csproj"
COPY . .
WORKDIR "AlugaCarros.Locacoes.Api"
RUN dotnet build "AlugaCarros.Locacoes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlugaCarros.Locacoes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlugaCarros.Locacoes.Api.dll"]