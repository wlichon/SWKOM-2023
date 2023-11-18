#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081

# Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

# Copy the code into the container
WORKDIR .
COPY ["src/NPaperless.Services/NPaperless.Services.csproj", "NPaperless.Services/"]

COPY ["ClassLibrary/NPaperless.Models.csproj", "ClassLibrary/"]

COPY ["NPaperless.DataAccess.Entities", "NPaperless.DataAccess.Entities/"]

COPY ["NPaperless.DataAccess.Interfaces", "NPaperless.DataAccess.Interfaces/"]




# NuGet restore
RUN dotnet restore "NPaperless.Services/NPaperless.Services.csproj"
RUN dotnet restore "ClassLibrary/NPaperless.Models.csproj"
RUN dotnet restore "NPaperless.DataAccess.Entities/NPaperless.DataAccess.Entities.csproj"
RUN dotnet restore "NPaperless.DataAccess.Interfaces/NPaperless.DataAccess.Interfaces.csproj"

COPY ["src/NPaperless.Services/", "NPaperless.Services/"]
COPY ["ClassLibrary/", "ClassLibrary/"]
COPY ["NPaperless.DataAccess.Entities/", "NPaperless.DataAccess.Entities/"]
COPY ["NPaperless.DataAccess.Interfaces/", "NPaperless.DataAccess.Interfaces/"]

RUN dotnet publish "NPaperless.Services/NPaperless.Services.csproj" -c Release -o /app/publish

# Publish it
# Make the final image for publishing
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.dll"]
