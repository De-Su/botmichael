FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BotMichael.Host/BotMichael.Host.csproj", "BotMichael.Host/"]
COPY ["src/BotMichael.Domain/BotMichael.Domain.csproj", "BotMichael.Domain/"]
COPY ["src/BotMichael.Application/BotMichael.Application.csproj", "BotMichael.Application/"]
RUN dotnet restore "src/BotMichael.Host/BotMichael.Host.csproj"
COPY . .
WORKDIR "/src/BotMichael.Host"
RUN dotnet build "BotMichael.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BotMichael.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotMichael.Host.dll"]