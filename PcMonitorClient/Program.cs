using PcMonitorClient.Services;
using System;
using System.Threading.Tasks;

namespace PcMonitorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Logger.Info("Запуск программы.");

            try
            {
                string configPath = "config.json";
                ConfigReader config = ConfigReader.LoadConfig(configPath);

                InfoCollector collector = new InfoCollector(config);
                await collector.CollectAndSendInfoAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка выполнения программы: {ex.Message}");
            }

            Logger.Info("Программа завершена.");
        }
    }
}
