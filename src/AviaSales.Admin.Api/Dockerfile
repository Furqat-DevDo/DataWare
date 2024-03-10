FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AviaSales.API/AviaSales.API.csproj", "src/AviaSales.API/"]
RUN dotnet restore "src/AviaSales.API/AviaSales.API.csproj"
COPY . .
WORKDIR "/src/src/AviaSales.API"
RUN dotnet build "AviaSales.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AviaSales.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AviaSales.API.dll"]
