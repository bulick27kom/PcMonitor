using PcMonitorClient.Services;
using System;
using System.Collections.Generic;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для сбора информации о системе.
    /// </summary>
    public class SystemInfoCollector
    {
        private ProcessorInfo _processorInfo = new ProcessorInfo();
        private MemoryInfo _memoryInfo = new MemoryInfo();
        private DiskInfo _diskInfo = new DiskInfo();
        private GraphicsInfo _graphicsInfo = new GraphicsInfo();
        private SoftwareInfo _softwareInfo = new SoftwareInfo();
        private NetworkInfo _networkInfo = new NetworkInfo();

        /// <summary>
        /// Собирает информацию о системе.
        /// </summary>
        /// <returns>Объект с информацией.</returns>
        public object CollectSystemInfo()
        {
            Logger.Info("Сбор информации о системе...");

            string computerName = _softwareInfo.GetComputerName();
            var processor = _processorInfo.GetProcessorInfo();

            return new
            {
                ComputerName = computerName,
                Processor = new
                {
                    Name = processor.Name,
                    Cores = processor.Cores,
                    Threads = processor.Threads
                },
                MemoryModules = _memoryInfo.GetMemoryInfo(),
                DiskDrives = _diskInfo.GetDiskInfo(),
                GraphicsCards = _graphicsInfo.GetGraphicsInfo(),
                OS = _softwareInfo.GetOSInfo(),
                SystemArchitecture = _softwareInfo.GetSystemArchitecture(),
                WorkgroupOrDomain = _softwareInfo.GetWorkgroupOrDomain(),
                LastBootTime = _softwareInfo.GetLastBootTime(),
                NetworkInterfaces = _networkInfo.GetNetworkInfo()
            };
        }
    }
}
