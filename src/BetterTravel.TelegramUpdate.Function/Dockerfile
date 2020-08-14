ARG DotnetVersion=3.1
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS installer-env
WORKDIR /app

# Copy csproj files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/${file%.*}/ \
        && mv $file src/${file%.*}/; \
    done

# Restore api csproj
RUN dotnet restore ./src/BetterTravel.TelegramUpdate.Function/BetterTravel.TelegramUpdate.Function.csproj

# Copy everything else and build
COPY . ./

# Publish to out folder
RUN dotnet publish ./src/BetterTravel.TelegramUpdate.Function/BetterTravel.TelegramUpdate.Function.csproj -c Release --no-restore -o /app/out

RUN mkdir -p /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:3.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 8443/tcp
COPY --from=installer-env ["/app/out", "/home/site/wwwroot"]