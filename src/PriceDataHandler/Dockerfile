#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-jammy-arm64v8 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy-arm64v8 AS build
WORKDIR /src
COPY ["src/PriceDataHandler/PriceDataHandler.csproj", "src/PriceDataHandler/"]
RUN dotnet restore "src/PriceDataHandler/PriceDataHandler.csproj"
COPY . .
WORKDIR "/src/src/PriceDataHandler"
RUN dotnet build "PriceDataHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PriceDataHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceDataHandler.dll"]