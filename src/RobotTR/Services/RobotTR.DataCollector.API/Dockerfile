#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./RobotTR/Services/RobotTR.DataCollector.API/RobotTR.DataCollector.API.csproj", "Services/RobotTR.DataCollector.API/"]
COPY ["./RobotTR/BuildingBlocks/Services/RobotTR.WebAPI.Core/RobotTR.WebAPI.Core.csproj", "BuildingBlocks/Services/RobotTR.WebAPI.Core/"]
COPY ["./RobotTR/BuildingBlocks/Core/RobotTR.Core/RobotTR.Core.csproj", "BuildingBlocks/Core/RobotTR.Core/"]
RUN dotnet restore "Services/RobotTR.DataCollector.API/RobotTR.DataCollector.API.csproj"
COPY . .
WORKDIR "RobotTR/Services/RobotTR.DataCollector.API"
RUN dotnet build "RobotTR.DataCollector.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobotTR.DataCollector.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotTR.DataCollector.API.dll"]