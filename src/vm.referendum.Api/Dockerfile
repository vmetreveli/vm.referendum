# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy entire solution (prevents missing csproj issues)
COPY . .

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
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Railway provides PORT dynamically (important)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Start application
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]