using System;
using System.IO;
using Newtonsoft.Json;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для чтения конфигурации.
    /// </summary>
    public class ConfigReader
    {
        public string ServiceAddress { get; set; }
        public string ServiceName { get; set; }
        public string ControllerName { get; set; }
        public int RequestTimeout { get; set; }

        /// <summary>
        /// Загружает конфигурацию из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу конфигурации.</param>
        /// <returns>Объект конфигурации.</returns>
        public static ConfigReader LoadConfig(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Файл конфигурации не найден: {filePath}");
                }

                string json = File.ReadAllText(filePath);
                ConfigReader config = JsonConvert.DeserializeObject<ConfigReader>(json);

                if (config == null)
                {
                    throw new Exception("Ошибка чтения конфигурации: не удалось десериализовать JSON.");
                }

                if (string.IsNullOrEmpty(config.ControllerName))
                {
                    throw new Exception("Ошибка чтения конфигурации: отсутствует параметр 'ControllerName'.");
                }

                if (string.IsNullOrEmpty(config.ServiceAddress) && string.IsNullOrEmpty(config.ServiceName))
                {
                    throw new Exception("Ошибка чтения конфигурации: отсутствует 'ServiceAddress' и 'ServiceName'. Укажите хотя бы один параметр.");
                }

                if (config.RequestTimeout <= 0)
                {
                    throw new Exception("Ошибка чтения конфигурации: 'RequestTimeout' должен быть больше 0.");
                }

                Logger.Info("Конфигурация загружена успешно.");
                return config;
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка загрузки конфигурации: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Возвращает базовый URL для API (учитывает приоритет `ServiceName` над `ServiceAddress`).
        /// </summary>
        public string GetBaseUrl()
        {
            return !string.IsNullOrEmpty(ServiceName) ? $"https://{ServiceName}" : $"https://{ServiceAddress}";
        }
    }
}
