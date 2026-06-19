# ==========================================
# Etapa 1: Compilación (SDK de .NET 9)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copiar absolutamente todo el código de la solución desde el principio
COPY . .

# 2. Restaurar dependencias de forma explícita para toda la solución
RUN dotnet restore "ColorexApp.sln"

# 3. Moverse al proyecto WebApi, restaurar y publicar todo junto
WORKDIR "/src/ColorexApp.WebApi"
RUN dotnet publish "ColorexApp.WebApi.csproj" -c Release -o /app/publish

# ==========================================
# Etapa 2: Entorno de Ejecución (Runtime de .NET 9)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copiar únicamente los binarios compilados de la etapa anterior
COPY --from=build /app/publish .

# Exponer el puerto que Render exige por defecto para contenedores de Linux
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Comando de arranque para iniciar la API de Colorex
ENTRYPOINT ["dotnet", "ColorexApp.WebApi.dll"]