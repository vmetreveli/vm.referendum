# syntax=docker/dockerfile:1.6

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8181
EXPOSE 8181

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -m appuser
USER appuser

# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

ARG GITHUB_TOKEN

COPY . .

# IMPORTANT: restore FIRST
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj"

# FIXED PATH (no backslashes, no WORKDIR confusion)
RUN dotnet build "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

# =========================
FROM build AS publish

RUN dotnet publish "src/vm.referendum.Api/vm.referendum.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

# =========================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK CMD curl --fail http://localhost:8181/health || exit 1

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]