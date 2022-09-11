FROM mcr.microsoft.com/dotnet/runtime:3.1-focal AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
WORKDIR /src
COPY ["maramora_tg_bot/Kropka_bot/Kropka_bot/Kropka_bot.csproj", "maramora_tg_bot/Kropka_bot/Kropka_bot/"]
RUN dotnet restore "maramora_tg_bot/Kropka_bot/Kropka_bot/Kropka_bot.csproj"
COPY . .
WORKDIR "/src/maramora_tg_bot/Kropka_bot/Kropka_bot"
RUN dotnet build "Kropka_bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kropka_bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kropka_bot.dll"]
