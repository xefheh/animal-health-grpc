FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AnimalHealth.API/AnimalHealth.API.csproj", "AnimalHealth.API/"]
COPY ["AnimalHealth.Domain/AnimalHealth.Domain.csproj", "AnimalHealth.Domain/"]
COPY ["AnimalHealth.Persistence/AnimalHealth.Persistence.csproj", "AnimalHealth.Persistence/"]
COPY ["AnimalHealth.Application/AnimalHealth.Application.csproj", "AnimalHealth.Application/"]
RUN dotnet restore "AnimalHealth.API/AnimalHealth.API.csproj"
COPY . .
WORKDIR "/src/AnimalHealth.API"
RUN dotnet build "AnimalHealth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimalHealth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalHealth.API.dll"]
