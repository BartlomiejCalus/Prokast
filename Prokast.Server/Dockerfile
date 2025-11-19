# Etap 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiujemy tylko plik csproj, żeby warstwa restore dobrze się keszowała
COPY *.csproj ./
RUN dotnet restore

# Kopiujemy cały kod
COPY . .
RUN dotnet publish Prokast.Server.csproj -c Release -o /app/publish

# Etap 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Prokast.Server.dll"]
