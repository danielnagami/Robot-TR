#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./RobotTR/Web/RobotTR.Portal.MVC/RobotTR.Portal.MVC.csproj", "Web/RobotTR.Portal.MVC/"]
COPY ["./RobotTR/BuildingBlocks/Services/RobotTR.WebAPI.Core/RobotTR.WebAPI.Core.csproj", "BuildingBlocks/Services/RobotTR.WebAPI.Core/"]
COPY ["./RobotTR/BuildingBlocks/Core/RobotTR.Core/RobotTR.Core.csproj", "BuildingBlocks/Core/RobotTR.Core/"]
RUN dotnet restore "Web/RobotTR.Portal.MVC/RobotTR.Portal.MVC.csproj"
COPY . .
WORKDIR "RobotTR/Web/RobotTR.Portal.MVC"
RUN dotnet build "RobotTR.Portal.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobotTR.Portal.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobotTR.Portal.MVC.dll"]