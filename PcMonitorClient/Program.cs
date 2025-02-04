using PcMonitorClient.Services;
using System;
using System.Threading.Tasks;

namespace PcMonitorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Logger.Log("Запуск программы.");

            try
            {
                string configPath = "config.json";
                ConfigReader config = ConfigReader.LoadConfig(configPath);

                InfoCollector collector = new InfoCollector(config);
                await collector.CollectAndSendInfoAsync();
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка выполнения программы: {ex.Message}");
            }

            Logger.Log("Программа завершена.");
        }
    }
}
