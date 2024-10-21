# Base stage to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expose the ports the app will run on
EXPOSE 8080
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

# Copy the .csproj files and restore dependencies for each project
COPY ["vm.referendum.Api/vm.referendum.Api.csproj", "vm.referendum.Api/"]
COPY ["Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "Framework/Framework.Infrastructure/"]
COPY ["Framework/Framework.Abstractions/Framework.Abstractions.csproj", "Framework/Framework.Abstractions/"]
COPY ["vm.referendum.Application/vm.referendum.Application.csproj", "vm.referendum.Application/"]
COPY ["vm.referendum.Domain/vm.referendum.Domain.csproj", "vm.referendum.Domain/"]
COPY ["Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "Infrastructure/vm.referendum.Infrastructure/"]

# Restore dependencies for the API project
RUN dotnet restore "vm.referendum.Api/vm.referendum.Api.csproj"

# Copy all source files
COPY . .

# Set the working directory for the build and run the build command
WORKDIR "/src/vm.referendum.Api"
RUN dotnet build "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the app
FROM base AS final
WORKDIR /app

# Copy published files from the build stage
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]
