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
COPY ["ProductivIO.Backend.csproj", "./"]
RUN dotnet restore "ProductivIO.Backend.csproj"
COPY . .
RUN dotnet build "ProductivIO.Backend.csproj" -c $configuration -o /app/build
 
# Publish stage
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ProductivIO.Backend.csproj" -c $configuration -o /app/publish /p:UseAppHost=false
 
# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductivIO.Backend.dll"]