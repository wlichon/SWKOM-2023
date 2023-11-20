#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8081

# Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

# Copy the code into the container
WORKDIR .
COPY ["NPaperless.Services/NPaperless.Services.csproj", "NPaperless.Services/"]

COPY ["NPaperless.Models/NPaperless.Models.csproj", "ClassLibrary/"]

COPY ["NPaperless.DataAccess.Entities", "NPaperless.DataAccess.Entities/"]

COPY ["NPaperless.DataAccess.Interfaces", "NPaperless.DataAccess.Interfaces/"]

COPY ["NPaperless.BusinessLogic", "NPaperless.BusinessLogic/"]


COPY ["NPaperless.Services/", "NPaperless.Services/"]
COPY ["NPaperless.Models/", "NPaperless.Models/"]
COPY ["NPaperless.DataAccess.Entities/", "NPaperless.DataAccess.Entities/"]
COPY ["NPaperless.DataAccess.Interfaces/", "NPaperless.DataAccess.Interfaces/"]
COPY ["NPaperless.BusinessLogic/", "NPaperless.BusinessLogic/"]

RUN dotnet publish "NPaperless.Services/NPaperless.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.Services.dll"]
