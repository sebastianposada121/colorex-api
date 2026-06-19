# ==========================================
# Etapa 1: Compilación (SDK de .NET)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar los archivos de proyecto (.csproj) desde la raíz real
COPY ["ColorexApp.Domain/ColorexApp.Domain.csproj", "ColorexApp.Domain/"]
COPY ["ColorexApp.Application/ColorexApp.Application.csproj", "ColorexApp.Application/"]
COPY ["ColorexApp.Infrastructure/ColorexApp.Infrastructure.csproj", "ColorexApp.Infrastructure/"]
COPY ["ColorexApp.WebApi/ColorexApp.WebApi.csproj", "ColorexApp.WebApi/"]

# 2. Restaurar los paquetes NuGet basados en la ruta real
RUN dotnet restore "ColorexApp.WebApi/ColorexApp.WebApi.csproj"

# 3. Copiar el resto del código fuente de la solución
COPY . .

# 4. Compilar y publicar los binarios optimizados desde la raíz real
WORKDIR "/src/ColorexApp.WebApi"
RUN dotnet publish "ColorexApp.WebApi.csproj" -c Release -o /app/publish --no-restore

# ==========================================
# Etapa 2: Entorno de Ejecución (Runtime)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar únicamente los binarios compilados de la etapa anterior
COPY --from=build /app/publish .

# Exponer el puerto que Render exige por defecto para contenedores de Linux
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Comando de arranque para iniciar la API de Colorex
ENTRYPOINT ["dotnet", "ColorexApp.WebApi.dll"]