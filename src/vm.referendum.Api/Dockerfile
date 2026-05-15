# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# GitHub token (build-time only)
ARG GITHUB_TOKEN

# Copy solution first (better cache)
COPY . .

# =========================
# Configure GitHub Packages securely (BuildKit-friendly)
# =========================
RUN dotnet nuget add source \
    https://nuget.pkg.github.com/vmetreveli/index.json \
    --name GitHub \
    --username vmetreveli \
    --password $GITHUB_TOKEN \
    --store-password-in-clear-text

# Restore dependencies
RUN dotnet restore src/vm.referendum.Api/vm.referendum.Api.csproj

# Publish release build
RUN dotnet publish src/vm.referendum.Api/vm.referendum.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]