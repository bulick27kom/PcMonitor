using PcMonitorClient;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Настроить логирование
        LogManager.ConfigureLogging();

        // Пример логирования
        await LogMessage("Запуск программы.");

        // Сбор и отправка данных
        var collector = new InfoCollector();
        await collector.CollectAndSaveInfo();

        // Пример логирования завершения
        await LogMessage("Программа завершена.");

        // Закрыть логирование в конце программы
        LogManager.Close();
    }

    // Асинхронная запись в лог
    static async Task LogMessage(string message)
    {
        await LogManager.LogMessageAsync(message);
    }
}
