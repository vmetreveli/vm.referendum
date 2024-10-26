FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/vm.referendum.Api/vm.referendum.Api.csproj", "vm.referendum.Api/"]
COPY ["/vm.referendum.Application/vm.referendum.Application.csproj", "vm.referendum.Application/"]
COPY ["/vm.referendum.Domain/vm.referendum.Domain.csproj", "vm.referendum.Domain/"]
COPY ["/Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "Framework/Framework.Infrastructure/"]
COPY ["/Framework/Framework.Abstractions/Framework.Abstractions.csproj", "Framework/Framework.Abstractions/"]
COPY ["/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "Infrastructure/vm.referendum.Infrastructure/"]
RUN dotnet restore "vm.referendum.Api/vm.referendum.Api.csproj"
COPY . .
WORKDIR "/src/vm.referendum.Api"
RUN dotnet build "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]
