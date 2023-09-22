FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationPattern/NotificationPattern.csproj", "NotificationPattern/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "NotificationPattern/NotificationPattern.csproj"
COPY . .
WORKDIR "/src/NotificationPattern"
RUN dotnet build "NotificationPattern.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationPattern.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Infra/DatabaseScripts/InitDatabase.sql", "infra/"]
ENTRYPOINT ["dotnet", "NotificationPattern.dll"]