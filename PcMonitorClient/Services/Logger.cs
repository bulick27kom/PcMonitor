using Serilog;
using System;
using System.IO;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для логирования операций с использованием Serilog.
    /// </summary>
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        private const int MaxLogSizeMB = 5;
        private const int LogRetentionDays = 30;

        static Logger()
        {
            ConfigureLogger();
        }

        /// <summary>
        /// Настраивает Serilog для работы с файлами логов.
        /// </summary>
        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    LogFilePath,
                    retainedFileCountLimit: LogRetentionDays, // Хранить только 30 дней логов
                    fileSizeLimitBytes: MaxLogSizeMB * 1024 * 1024, // 5MB на файл
                    rollOnFileSizeLimit: true, // Автоматическая ротация при превышении размера
                    shared: true) // Разделяемый доступ к файлу (нет блокировки)
                .CreateLogger();
        }

        /// <summary>
        /// Записывает информационное сообщение в лог.
        /// </summary>
        public static void Info(string message)
        {
            Log.Information(message);
        }

        /// <summary>
        /// Записывает предупреждение в лог.
        /// </summary>
        public static void Warn(string message)
        {
            Log.Warning(message);
        }

        /// <summary>
        /// Записывает ошибку в лог.
        /// </summary>
        public static void Error(string message, Exception ex = null)
        {
            Log.Error(ex, message);
        }

        /// <summary>
        /// Освобождает ресурсы перед завершением программы.
        /// </summary>
        public static void Close()
        {
            Log.CloseAndFlush();
        }
    }
}
