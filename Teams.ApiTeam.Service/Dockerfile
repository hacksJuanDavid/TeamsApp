FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 4001
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Teams.ApiTeam.Service/Teams.ApiTeam.Service.csproj", "Teams.ApiTeam.Service/"]
RUN dotnet restore "Teams.ApiTeam.Service/Teams.ApiTeam.Service.csproj"
COPY . .
WORKDIR "/src/Teams.ApiTeam.Service"
RUN dotnet build "Teams.ApiTeam.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.ApiTeam.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.ApiTeam.Service.dll"]
