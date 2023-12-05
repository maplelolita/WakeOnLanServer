FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["WolServer.csproj", ""]
RUN dotnet restore "./WolServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WolServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WolServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WolServer.dll"]