using PcMonitorClient.Services;
using System;
using System.Threading.Tasks;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс, координирующий сбор, сохранение и отправку данных.
    /// </summary>
    public class InfoCollector
    {
        private readonly SystemInfoCollector _systemInfoCollector;
        private readonly FileManager _fileManager;
        private readonly WebServiceSender _webServiceSender;
        private readonly ConfigReader _config;

        public InfoCollector(ConfigReader config)
        {
            _config = config;
            _systemInfoCollector = new SystemInfoCollector();
            _fileManager = new FileManager();
            _webServiceSender = new WebServiceSender(config);
        }

        /// <summary>
        /// Выполняет сбор, сохранение и отправку данных.
        /// </summary>
        public async Task CollectAndSendInfoAsync()
        {
            Logger.Log("Начало сбора информации...");

            var systemInfo = _systemInfoCollector.CollectSystemInfo();
            string computerName = ((dynamic)systemInfo).ComputerName;

            Task saveTask = Task.Run(() => _fileManager.SaveToFile(systemInfo, computerName));
            Task sendTask = _webServiceSender.SendToWebServiceAsync(systemInfo);

            await Task.WhenAll(saveTask, sendTask);

            Logger.Log("Операции сбора, сохранения и отправки завершены.");
        }
    }
}
