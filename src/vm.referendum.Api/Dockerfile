# syntax=docker/dockerfile:1

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

# Build argument for GitHub Packages authentication
ARG GITHUB_TOKEN

# ============================================================
# Copy project files first (cache layer)
# ============================================================
COPY src/vm.referendum.Api/vm.referendum.Api.csproj src/vm.referendum.Api/
COPY src/vm.referendum.Application/vm.referendum.Application.csproj src/vm.referendum.Application/
COPY src/vm.referendum.Domain/vm.referendum.Domain.csproj src/vm.referendum.Domain/
COPY src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj src/Infrastructure/vm.referendum.Infrastructure/
COPY src/NuGet.config ./

# ============================================================
# Inject GitHub token into NuGet.config
# Replace %GITHUB_TOKEN% placeholder in NuGet.config
# ============================================================
RUN sed -i "s|%GITHUB_TOKEN%|${GITHUB_TOKEN}|g" NuGet.config

# ============================================================
# Restore dependencies
# ============================================================
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    --configfile NuGet.config

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
     /p:UseAppHost=false

# ============================================================
# Final runtime
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK CMD curl --fail http://localhost:8181/health || exit 1

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]