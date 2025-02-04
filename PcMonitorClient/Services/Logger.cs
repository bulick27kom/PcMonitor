// Logger.cs
using System;
using System.IO;
using System.Linq;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для логирования операций с автоматическим управлением размером и очисткой старых записей.
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        private const int MaxLogSizeMB = 5;
        private const int MaxLogSizeBytes = MaxLogSizeMB * 1024 * 1024;
        private const int LogRetentionDays = 30;

        /// <summary>
        /// Записывает сообщение в лог-файл с меткой даты и времени.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public static void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            string logEntry = $"{timestamp} - {message}";

            try
            {
                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
                ManageLogFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи лога: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаляет записи старше 30 дней и контролирует размер файла.
        /// </summary>
        private static void ManageLogFile()
        {
            try
            {
                if (!File.Exists(LogFilePath)) return;

                var lines = File.ReadAllLines(LogFilePath).ToList();
                DateTime cutoffDate = DateTime.Now.AddDays(-LogRetentionDays);

                // Фильтруем старые записи
                lines = lines.Where(line =>
                {
                    if (line.Length < 19) return false; // Пропускаем некорректные строки
                    if (DateTime.TryParseExact(line.Substring(0, 19), "dd.MM.yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime logDate))
                    {
                        return logDate >= cutoffDate;
                    }
                    return false;
                }).ToList();

                // Проверяем размер файла
                if (lines.Count > 0)
                {
                    File.WriteAllLines(LogFilePath, lines);
                    if (new FileInfo(LogFilePath).Length > MaxLogSizeBytes)
                    {
                        File.WriteAllLines(LogFilePath, lines.Skip(lines.Count / 2)); // Оставляем только половину записей
                    }
                }
                else
                {
                    File.Delete(LogFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при управлении лог-файлом: {ex.Message}");
            }
        }
    }
}
