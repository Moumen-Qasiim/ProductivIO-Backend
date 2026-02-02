# Use ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5104
ENV ASPNETCORE_URLS=http://+:5104
ENV ASPNETCORE_ENVIRONMENT=Development
 
 
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src

# 1. Copy the project file using the new subfolder path
COPY ["ProductivIO.Backend/ProductivIO.Backend.csproj", "ProductivIO.Backend/"]

# 2. Restore based on that path
RUN dotnet restore "ProductivIO.Backend/ProductivIO.Backend.csproj"

# 3. Copy everything else
COPY . .

# 4. Build the specific project
WORKDIR "/src/ProductivIO.Backend"
RUN dotnet build "ProductivIO.Backend.csproj" -c $configuration -o /app/build

# Publish stage
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ProductivIO.Backend.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Final stage (Assumes you have a 'base' image defined or use aspnet)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductivIO.Backend.dll"]