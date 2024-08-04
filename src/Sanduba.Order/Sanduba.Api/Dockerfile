#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_ConnectionStrings__OrderDatabase__Value=default
ENV ASPNETCORE_ConnectionStrings__OrderDatabase__Type=default
ENV ASPNETCORE_ConnectionStrings__CartDatabase__Value=default
ENV ASPNETCORE_ConnectionStrings__CartDatabase__Type=default
ENV ASPNETCORE_JwtSettings__SecretKey=default
ENV ASPNETCORE_JwtSettings__Issuer=default
ENV ASPNETCORE_JwtSettings__Audience=default
ENV ASPNETCORE_PaymentSettings__BaseUrl=default
ENV ASPNETCORE_BrokerSettings__ConnectionStrings=default

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sanduba.Order/Sanduba.Api/Sanduba.API.csproj", "Sanduba.Order/Sanduba.Api/"]
COPY ["Sanduba.Order/Sanduba.Core.Application/Sanduba.Core.Application.csproj", "Sanduba.Order/Sanduba.Core.Application/"]
COPY ["Sanduba.Order/Sanduba.Core.Application.Abstraction/Sanduba.Core.Application.Abstraction.csproj", "Sanduba.Order/Sanduba.Core.Application.Abstraction/"]
COPY ["Sanduba.Order/Sanduba.Core.Domain/Sanduba.Core.Domain.csproj", "Sanduba.Order/Sanduba.Core.Domain/"]
COPY ["Sanduba.Order/Sanduba.Adapter.Mvc/Sanduba.Adapter.Mvc.csproj", "Sanduba.Order/Sanduba.Adapter.Mvc/"]
COPY ["Sanduba.Order/Sanduba.Infrastructure.PaymentAPI/Sanduba.Infrastructure.API.Payment.csproj", "Sanduba.Order/Sanduba.Infrastructure.PaymentAPI/"]
COPY ["Sanduba.Order/Sanduba.Infrastructure.Persistence.Redis/Sanduba.Infrastructure.Persistence.Redis.csproj", "Sanduba.Order/Sanduba.Infrastructure.Persistence.Redis/"]
COPY ["Sanduba.Order/Sanduba.Infrastructure.Persistence.SqlServer/Sanduba.Infrastructure.Persistence.SqlServer.csproj", "Sanduba.Order/Sanduba.Infrastructure.Persistence.SqlServer/"]
RUN dotnet restore "./Sanduba.Order/Sanduba.Api/Sanduba.API.csproj"
COPY . .
WORKDIR "/src/Sanduba.Order/Sanduba.Api"
RUN dotnet build "./Sanduba.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sanduba.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanduba.API.dll"]