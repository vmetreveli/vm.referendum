# syntax=docker/dockerfile:1.6

# ============================================================
# Base runtime image
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV PORT=8181
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8181

USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

USER app

# ============================================================
# Build stage
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/vm.referendum.Api/vm.referendum.Api.csproj", "src/vm.referendum.Api/"]
COPY ["src/vm.referendum.Application/vm.referendum.Application.csproj", "src/vm.referendum.Application/"]
COPY ["src/vm.referendum.Domain/vm.referendum.Domain.csproj", "src/vm.referendum.Domain/"]
COPY ["src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "src/Infrastructure/vm.referendum.Infrastructure/"]

# 👇 IMPORTANT FIX: copy NuGet config into Docker
COPY NuGet.Config /root/.nuget/NuGet/NuGet.Config

# 👇 IMPORTANT FIX: force restore to use it
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    --configfile /root/.nuget/NuGet/NuGet.Config

# Copy everything else
COPY . .

WORKDIR /src/src/vm.referendum.Api

RUN dotnet build vm.referendum.Api.csproj -c Release -o /app/build --no-restore

# ============================================================
# Publish stage
# ============================================================
FROM build AS publish
RUN dotnet publish vm.referendum.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

# ============================================================
# Final runtime stage
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=10s --start-period=20s --retries=3 \
    CMD curl --fail http://localhost:${PORT}/health || exit 1

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]