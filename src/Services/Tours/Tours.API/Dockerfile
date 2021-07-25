FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Tours/ToursProvider/ToursProvider.fsproj", "ToursProvider/"]
COPY ["src/BuildingBlocks/Common/Common.fsproj", "Common/"]
RUN dotnet restore "src/Services/Tours/ToursProvider/ToursProvider.fsproj"
RUN dotnet restore "src/BuildingBlocks/Common/Common.fsproj"
COPY . .
WORKDIR "/src/Services/Tours/ToursProvider"
RUN dotnet build "ToursProvider.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToursProvider.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToursProvider.dll"]
