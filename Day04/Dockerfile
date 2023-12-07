FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Day04/Day04.csproj", "Day04/"]
RUN dotnet restore "Day04/Day04.csproj"
COPY . .
WORKDIR "/src/Day04"
RUN dotnet build "Day04.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Day04.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Day04.dll"]
