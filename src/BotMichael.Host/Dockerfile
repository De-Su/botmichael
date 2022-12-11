FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BotMichael.Host/BotMichael.Host.fsproj", "src/BotMichael.Host/"]
COPY ["src/BotMichael.Bot/BotMichael.Bot.fsproj", "src/BotMichael.Bot/"]
RUN dotnet restore "src/BotMichael.Host/BotMichael.Host.fsproj"
COPY . .
RUN dotnet build "src/BotMichael.Host/BotMichael.Host.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/BotMichael.Host/BotMichael.Host.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV BOT_TOKEN=${BOT_TOKEN}
ENTRYPOINT ["dotnet", "BotMichael.Host.dll"]
