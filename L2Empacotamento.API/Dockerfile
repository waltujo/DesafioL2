FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["L2Empacotamento.API/L2Empacotamento.API.csproj", "L2Empacotamento.API/"]
COPY ["L2Empacotamento.Application/L2Empacotamento.Application.csproj", "L2Empacotamento.Application/"]
COPY ["L2Empacotamento.Domain/L2Empacotamento.Domain.csproj", "L2Empacotamento.Domain/"]
COPY ["L2Empacotamento.Infrastructure/L2Empacotamento.Infrastructure.csproj", "L2Empacotamento.Infrastructure/"]
RUN dotnet restore "./L2Empacotamento.API/L2Empacotamento.API.csproj"

# Instalar o EF CLI
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY . .
WORKDIR "/src/L2Empacotamento.API"
RUN dotnet build "./L2Empacotamento.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./L2Empacotamento.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "L2Empacotamento.API.dll"]
