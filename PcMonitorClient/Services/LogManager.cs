using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

public static class LogManager
{
    private static readonly SemaphoreSlim _logSemaphore = new SemaphoreSlim(1, 1);

    // Инициализация логирования
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(a => a.File("log.txt", rollingInterval: RollingInterval.Day, buffered: true))
            .CreateLogger();
    }

    // Логирование с защитой от одновременной записи
    public static async Task LogMessageAsync(string message)
    {
        await _logSemaphore.WaitAsync(); // Ожидание блокировки

        try
        {
            // Логируем сообщение с временной меткой
            Log.Information("{Timestamp} - {Message}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), message);
        }
        catch (Exception ex)
        {
            // Логируем ошибку записи в лог
            Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
        }
        finally
        {
            _logSemaphore.Release(); // Освобождаем блокировку
        }
    }

    // Закрытие логгера
    public static void Close()
    {
        Log.CloseAndFlush();
    }
}
