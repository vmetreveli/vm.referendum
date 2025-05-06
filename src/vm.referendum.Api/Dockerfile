FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
# Use non-root user for security
USER app
ENV ASPNETCORE_HTTP_PORTS=8181
ENV ASPNETCORE_URLS=http://+:8181
EXPOSE 8181

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copy only project files first to optimize layer caching
COPY ["src/vm.referendum.Api/vm.referendum.Api.csproj", "vm.referendum.Api/"]
COPY ["src/vm.referendum.Application/vm.referendum.Application.csproj", "vm.referendum.Application/"]
COPY ["src/vm.referendum.Domain/vm.referendum.Domain.csproj", "vm.referendum.Domain/"]
COPY ["src/Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "Framework/Framework.Infrastructure/"]
COPY ["src/Framework/Framework.Abstractions/Framework.Abstractions.csproj", "Framework/Framework.Abstractions/"]
COPY ["src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "Infrastructure/vm.referendum.Infrastructure/"]
RUN dotnet restore "vm.referendum.Api/vm.referendum.Api.csproj"

# Copy everything else
COPY . .
WORKDIR "/src/vm.referendum.Api"
# Use Release configuration consistently
RUN dotnet build "vm.referendum.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vm.referendum.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Add health check
HEALTHCHECK CMD curl --fail http://localhost:8181/health || exit
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]