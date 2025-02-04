using System;
using System.IO;
using Newtonsoft.Json;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для сохранения информации в файл.
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Сохраняет JSON-данные в файл.
        /// </summary>
        /// <param name="data">Объект с данными.</param>
        /// <param name="computerName">Имя компьютера.</param>
        public void SaveToFile(object data, string computerName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                string fileName = $"{computerName}_info.json";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                File.WriteAllText(filePath, json);
                Logger.Log($"Информация сохранена в {filePath}");
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка сохранения файла: {ex.Message}");
            }
        }
    }
}
