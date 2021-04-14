FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Exchange/Exchange.API/Exchange.API.fsproj", "Exchange.API/"]
RUN dotnet restore "src/Services/Exchange/Exchange.API/Exchange.API.fsproj"
COPY . .
WORKDIR "/src/Exchange.API"
RUN dotnet build "Exchange.API.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exchange.API.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exchange.API.dll"]
