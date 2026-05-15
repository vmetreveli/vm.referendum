# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Pass GitHub token securely at build time
ARG GITHUB_TOKEN
ENV GITHUB_TOKEN=$GITHUB_TOKEN

# Copy solution + all projects
COPY . .

# Ensure NuGet config is available
COPY src/vm.referendum.Api/NuGet.Config .

# Restore dependencies (GitHub + nuget.org)
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