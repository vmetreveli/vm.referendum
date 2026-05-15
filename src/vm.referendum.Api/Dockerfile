# syntax=docker/dockerfile:1.6

# ============================================================
# Base runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8181
EXPOSE 8181

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -m appuser
USER appuser

# ============================================================
# Build stage
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# ============================================================
# Install GitHub Packages auth securely (BuildKit secret)
# ============================================================
# Requires: docker build --secret id=github_token,env=GITHUB_TOKEN
RUN --mount=type=secret,id=github_token \
    export GITHUB_TOKEN=$(cat /run/secrets/github_token) && \
    dotnet nuget add source "https://nuget.pkg.github.com/vmetreveli/index.json" \
      --name github \
      --username vmetreveli \
      --password "$GITHUB_TOKEN" \
      --store-password-in-clear-text

# ============================================================
# Copy project files (cache-friendly)
# ============================================================
COPY src/vm.referendum.Api/vm.referendum.Api.csproj src/vm.referendum.Api/
COPY src/vm.referendum.Application/vm.referendum.Application.csproj src/vm.referendum.Application/
COPY src/vm.referendum.Domain/vm.referendum.Domain.csproj src/vm.referendum.Domain/
COPY src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj src/Infrastructure/vm.referendum.Infrastructure/
COPY src/NuGet.config ./

# ============================================================
# Restore dependencies (IMPORTANT: before full copy)
# ============================================================
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj"

# ============================================================
# Copy full source
# ============================================================
COPY . .

# ============================================================
# Build
# ============================================================
RUN dotnet build "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

# ============================================================
# Publish
# ============================================================
FROM build AS publish

RUN dotnet publish "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

# ============================================================
# Final runtime
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=10s --start-period=20s --retries=3 \
    CMD curl --fail http://localhost:8181/health || exit 1

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]