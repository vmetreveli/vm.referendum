FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Ensure APP_UID is set or default to root (optional)
ARG APP_UID=0
USER $APP_UID

# Log base image info
RUN echo "Base image setup completed. Current user ID: $(id -u) | Current working directory: $(pwd)"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Log starting build process
RUN echo "Starting build process with configuration: ${BUILD_CONFIGURATION}"

# Copy project files and log each step
COPY ["vm.referendum.Api/vm.referendum.Api.csproj", "vm.referendum.Api/"]
RUN echo "Copied vm.referendum.Api.csproj"
COPY ["vm.referendum.Application/vm.referendum.Application.csproj", "vm.referendum.Application/"]
RUN echo "Copied vm.referendum.Application.csproj"
COPY ["vm.referendum.Domain/vm.referendum.Domain.csproj", "vm.referendum.Domain/"]
RUN echo "Copied vm.referendum.Domain.csproj"
COPY ["Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "Framework/Framework.Infrastructure/"]
RUN echo "Copied Framework.Infrastructure.csproj"
COPY ["Framework/Framework.Abstractions/Framework.Abstractions.csproj", "Framework/Framework.Abstractions/"]
RUN echo "Copied Framework.Abstractions.csproj"
COPY ["Infrastructure/vm.referendum.Infrastructure/vm.referendum.Infrastructure.csproj", "Infrastructure/vm.referendum.Infrastructure/"]
RUN echo "Copied vm.referendum.Infrastructure.csproj"

# Restore dependencies and log
RUN dotnet restore "vm.referendum.Api/vm.referendum.Api.csproj" && echo "Restore completed successfully."

# Copy all files and log the directory structure
COPY . .
RUN echo "Files copied. Directory structure after copying:" && ls -la /src

# Build the project and log
WORKDIR "/src/vm.referendum.Api"
RUN dotnet build "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build && echo "Build completed successfully."

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publish and log
RUN dotnet publish "vm.referendum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false && echo "Publish completed successfully."

# Debug step to verify contents
RUN echo "Contents of /app/publish after publish:" && ls -la /app/publish

FROM base AS final
WORKDIR /app

# Copy published files from the publish stage and log the result
COPY --from=publish /app/publish .
RUN echo "Files copied from publish stage. Final directory structure:" && ls -la /app

# Entry point for the application with logging
ENTRYPOINT ["dotnet", "vm.referendum.Api.dll"]
