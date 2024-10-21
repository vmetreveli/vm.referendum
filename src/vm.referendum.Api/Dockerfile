FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
# Expose the port the app will run on
EXPOSE 8080
EXPOSE 443



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src/src
COPY ["src/vm.referendum.Api/vm.referendum.Api.csproj", "src/vm.referendum.Api/"]
COPY ["src/Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "src/Framework/Framework.Infrastructure/"]
COPY ["src/Framework/Framework.Abstractions/Framework.Abstractions.csproj", "src/Framework/Framework.Abstractions/"]
COPY ["src/vm.referendum.Application/vm.referendum.Application.csproj", "src/vm.referendum.Application/"]
COPY ["src/vm.referendum.Domain/vm.referendum.Domain.csproj", "src/vm.referendum.Domain/"]
COPY ["src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "src/Infrastructure/vm.referendum.Infrastructure/"]
RUN dotnet restore "src/vm.referendum.Api/vm.referendum.Api.csproj"
COPY . .
WORKDIR /src/src/vm.referendum.Api
RUN dotnet build "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]
