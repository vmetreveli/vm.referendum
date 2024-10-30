FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Set environment variables to listen on both HTTP and HTTPS
ENV ASPNETCORE_HTTP_PORTS=80

ENV ASPNETCORE_URLS=http://+:80

# Copy SSL certificate if you have one (uncomment if you have a certificate)
# COPY ["path/to/certificate.pfx", "/https/aspnetapp.pfx"]
# ENV ASPNETCORE_Kestrel__Certificates__Default__Password="your-password"
# ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"

# Expose ports for HTTP and HTTPS
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/vm.referendum.Api/vm.referendum.Api.csproj", "vm.referendum.Api/"]
COPY ["src/vm.referendum.Application/vm.referendum.Application.csproj", "vm.referendum.Application/"]
COPY ["src/vm.referendum.Domain/vm.referendum.Domain.csproj", "vm.referendum.Domain/"]
COPY ["src/Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "Framework/Framework.Infrastructure/"]
COPY ["src/Framework/Framework.Abstractions/Framework.Abstractions.csproj", "Framework/Framework.Abstractions/"]
COPY ["src/Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "Infrastructure/vm.referendum.Infrastructure/"]
RUN dotnet restore "/src/vm.referendum.Api/vm.referendum.Api.csproj"
COPY . .
WORKDIR "src/vm.referendum.Api"
RUN dotnet build "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]
