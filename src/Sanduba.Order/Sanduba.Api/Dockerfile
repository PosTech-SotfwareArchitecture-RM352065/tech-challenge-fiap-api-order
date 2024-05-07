#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Api/Sanduba.API.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Api/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Infrastructure.PaymentAPI/Sanduba.Infrastructure.PaymentAPI.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Infrastructure.PaymentAPI/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Application/Sanduba.Core.Application.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Application/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Application.Abstraction/Sanduba.Core.Application.Abstraction.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Application.Abstraction/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Domain/Sanduba.Core.Domain.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Core.Domain/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Adapter.Controller/Sanduba.Adapter.Controller.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Adapter.Controller/"]
COPY ["tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Infrastructure.Persistence.SqlServer/Sanduba.Infrastructure.Persistence.SqlServer.csproj", "tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Infrastructure.Persistence.SqlServer/"]
RUN dotnet restore "./tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Api/Sanduba.API.csproj"
COPY . .
WORKDIR "/src/tech-challenge-fiap-api-order/src/Sanduba.Order/Sanduba.Api"
RUN dotnet build "./Sanduba.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sanduba.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanduba.API.dll"]