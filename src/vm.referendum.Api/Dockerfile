FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Referendum.Api/Referendum.Api.csproj", "src/Referendum.Api/"]
COPY ["src/Referendum.Application/Referendum.Application.csproj", "src/Referendum.Application/"]
COPY ["src/Referendum.Domain/Referendum.Domain.csproj", "src/Referendum.Domain/"]
COPY ["../Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "../Framework/Framework.Infrastructure/"]
COPY ["../Framework/Framework.Abstractions/Framework.Abstractions.csproj", "../Framework/Framework.Abstractions/"]
COPY ["src/Referendum.Infrastructure/Referendum.Infrastructure.csproj", "src/Referendum.Infrastructure/"]
RUN dotnet restore "src/Referendum.Api/Referendum.Api.csproj"
COPY . .
WORKDIR "/src/src/Referendum.Api"
RUN dotnet build "Referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Referendum.Api.dll"]
