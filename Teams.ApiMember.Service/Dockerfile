FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Teams.ApiMember.Service/Teams.ApiMember.Service.csproj", "Teams.ApiMember.Service/"]
RUN dotnet restore "Teams.ApiMember.Service/Teams.ApiMember.Service.csproj"
COPY . .
WORKDIR "/src/Teams.ApiMember.Service"
RUN dotnet build "Teams.ApiMember.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.ApiMember.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.ApiMember.Service.dll"]
