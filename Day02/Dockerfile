FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Day02/Day02.csproj", "Day02/"]
RUN dotnet restore "Day02/Day02.csproj"
COPY . .
WORKDIR "/src/Day02"
RUN dotnet build "Day02.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Day02.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Day02.dll"]
