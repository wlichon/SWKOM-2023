#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081

# Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

# Copy the code into the container
WORKDIR /src
COPY ["src/NPaperless.Services/NPaperless.Services.csproj", "NPaperless.Services/"]

# NuGet restore
RUN dotnet restore "NPaperless.Services/NPaperless.Services.csproj"
COPY ["src/NPaperless.Services/", "NPaperless.Services/"]

RUN dotnet publish "NPaperless.Services/NPaperless.Services.csproj" -c Release -o /app/publish

# Publish it
# Make the final image for publishing
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.dll"]
