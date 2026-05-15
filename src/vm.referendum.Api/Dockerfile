# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

ARG GITHUB_TOKEN
ENV GITHUB_TOKEN=$GITHUB_TOKEN

COPY . .

RUN dotnet restore src/vm.referendum.Api/vm.referendum.Api.csproj

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