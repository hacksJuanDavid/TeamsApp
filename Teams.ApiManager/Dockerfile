FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Teams.ApiManager/Teams.ApiManager.csproj", "Teams.ApiManager/"]
RUN dotnet restore "Teams.ApiManager/Teams.ApiManager.csproj"
COPY . .
WORKDIR "/src/Teams.ApiManager"
RUN dotnet build "Teams.ApiManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.ApiManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.ApiManager.dll"]
