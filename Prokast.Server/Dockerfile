# Etap 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Skopiuj projekt API i przywróć zależności
COPY Prokast.Server.csproj ./
RUN dotnet restore Prokast.Server.csproj

# Skopiuj resztę kodu projektu
COPY . ./
RUN dotnet publish Prokast.Server.csproj -c Release -o /app/publish

# Etap 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Prokast.Server.dll"]