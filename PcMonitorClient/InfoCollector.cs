using PcMonitorClient;
using System;
using System.IO;
using System.Threading.Tasks;

public class InfoCollector
{
    public async Task CollectAndSaveInfo()
    {
        try
        {
            await LogMessage("Начало сбора информации...");

            // Пример сбора информации
            var systemInfo = new SystemInfo();
            var memoryInfo = new MemoryInfo();

            // Здесь будет сбор и сохранение данных

            await LogMessage("Информация собрана успешно.");
        }
        catch (Exception ex)
        {
            await LogMessage($"Ошибка при сборе информации: {ex.Message}");
        }
    }

    // Асинхронная запись в лог
    static async Task LogMessage(string message)
    {
        await LogManager.LogMessageAsync(message);
    }
}
