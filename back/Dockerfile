# syntax=docker/dockerfile:1

################################################################################
# BUILD STAGE
################################################################################

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /

# Copy project files individually for dependency caching
COPY AltenShop.API/AltenShop.API.csproj AltenShop.API/
COPY AltenShop.Application/AltenShop.Application.csproj AltenShop.Application/
COPY AltenShop.Domain/AltenShop.Domain.csproj AltenShop.Domain/
COPY AltenShop.Infrastructure/AltenShop.Infrastructure.csproj AltenShop.Infrastructure/
COPY AltenShop.Tests/AltenShop.Tests.csproj AltenShop.Tests/

# Restore dependencies
RUN dotnet restore AltenShop.API/AltenShop.API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish the API
RUN dotnet publish AltenShop.API/AltenShop.API.csproj -c Release -o /app/publish --no-restore

################################################################################
# FINAL STAGE
################################################################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "AltenShop.API.dll"]
