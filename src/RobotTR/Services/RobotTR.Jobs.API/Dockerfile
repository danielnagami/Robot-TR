#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/RobotTR.Jobs.API/RobotTR.Jobs.API.csproj", "Services/RobotTR.Jobs.API/"]
COPY ["BuildingBlocks/Services/RobotTR.WebAPI.Core/RobotTR.WebAPI.Core.csproj", "BuildingBlocks/Services/RobotTR.WebAPI.Core/"]
COPY ["BuildingBlocks/Core/RobotTR.Core/RobotTR.Core.csproj", "BuildingBlocks/Core/RobotTR.Core/"]
RUN dotnet restore "Services/RobotTR.Jobs.API/RobotTR.Jobs.API.csproj"
COPY . .
WORKDIR "/src/Services/RobotTR.Jobs.API"
RUN dotnet build "RobotTR.Jobs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobotTR.Jobs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotTR.Jobs.API.dll"]