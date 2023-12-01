FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Day01/Day01.csproj", "Day01/"]
RUN dotnet restore "Day01/Day01.csproj"
COPY . .
WORKDIR "/src/Day01"
RUN dotnet build "Day01.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Day01.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Day01.dll"]
