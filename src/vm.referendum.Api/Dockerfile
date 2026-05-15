# syntax=docker/dockerfile:1.6

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

# restore using secret (NO TOKEN in image layers)
RUN --mount=type=secret,id=github_token \
    dotnet restore src/vm.referendum.Api/vm.referendum.Api.csproj

RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]