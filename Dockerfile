# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY ["PcMonitorWebApi/PcMonitorWebApi.csproj", "PcMonitorWebApi/"]
RUN dotnet restore "PcMonitorWebApi/PcMonitorWebApi.csproj"

# Копируем весь исходный код и собираем приложение
COPY . .
WORKDIR "/app/PcMonitorWebApi"
RUN dotnet publish -c Release -o /app/publish

# Используем минимальный runtime-образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Запускаем приложение
CMD ["dotnet", "PcMonitorWebApi.dll"]
