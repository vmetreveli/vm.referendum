# syntax=docker/dockerfile:1.6

# ============================================================
# Base runtime image
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Railway + local compatibility
ENV PORT=8181
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8181

# Install curl for health checks
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

# Use built-in non-root user (DO NOT recreate it)
USER app

# ============================================================
# Build stage
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files (for caching)
COPY ["src/vm.referendum.Api/vm.referendum.Api.csproj", "src/vm.referendum.Api/"]
COPY ["src/vm.referendum.Application/vm.referendum.Application.csproj", "src/vm.referendum.Application/"]
COPY ["src/vm.referendum.Domain/vm.referendum.Domain.csproj", "src/vm.referendum.Domain/"]
COPY ["src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "src/Infrastructure/vm.referendum.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj"

# Copy everything else
COPY . .

# Build
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

# Health check (optional but useful)
HEALTHCHECK --interval=30s --timeout=10s --start-period=20s --retries=3 \
    CMD curl --fail http://localhost:${PORT}/health || exit 1

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]