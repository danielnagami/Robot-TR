#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./RobotTR/Services/RobotTR.Authentication.API/RobotTR.Authentication.API.csproj", "Services/RobotTR.Authentication.API/"]
COPY ["./RobotTR/BuildingBlocks/Services/RobotTR.WebAPI.Core/RobotTR.WebAPI.Core.csproj", "BuildingBlocks/Services/RobotTR.WebAPI.Core/"]
COPY ["./RobotTR/BuildingBlocks/Core/RobotTR.Core/RobotTR.Core.csproj", "BuildingBlocks/Core/RobotTR.Core/"]
COPY ["./RobotTR/BuildingBlocks/MessageBus/RobotTR.MessageBus/RobotTR.MessageBus.csproj", "BuildingBlocks/MessageBus/RobotTR.MessageBus/"]
RUN dotnet restore "Services/RobotTR.Authentication.API/RobotTR.Authentication.API.csproj"
COPY . .
WORKDIR "RobotTR/Services/RobotTR.Authentication.API"
RUN dotnet build "RobotTR.Authentication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobotTR.Authentication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotTR.Authentication.API.dll"]