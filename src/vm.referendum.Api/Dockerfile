# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only project files first (better caching)
COPY src/vm.referendum.Api/*.csproj src/vm.referendum.Api/
COPY src/vm.referendum.Application/*.csproj src/vm.referendum.Application/
COPY src/vm.referendum.Domain/*.csproj src/vm.referendum.Domain/
COPY src/vm.referendum.Infrastructure/*.csproj src/vm.referendum.Infrastructure/

# Restore dependencies (SAFE path)
RUN dotnet restore src/vm.referendum.Api/vm.referendum.Api.csproj

# Copy everything else
COPY . .

# Publish
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

# Railway / cloud port binding
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]