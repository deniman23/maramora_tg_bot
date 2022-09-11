#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Kropka_bot/Kropka_bot.csproj", "Kropka_bot/"]
RUN dotnet restore "Kropka_bot/Kropka_bot.csproj"
COPY . .
WORKDIR "/src/Kropka_bot"
RUN dotnet build "Kropka_bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kropka_bot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kropka_bot.dll"]
