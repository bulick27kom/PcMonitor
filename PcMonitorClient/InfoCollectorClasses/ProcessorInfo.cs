// ProcessorInfo.cs
using System.Management;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для получения информации о процессоре с использованием WMI.
    /// </summary>
    public class ProcessorInfo
    {
        /// <summary>
        /// Получить информацию о процессоре.
        /// </summary>
        /// <returns>Объект с названием процессора, количеством ядер и потоков.</returns>
        public ProcessorDetails GetProcessorInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name, NumberOfCores, NumberOfLogicalProcessors FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                return new ProcessorDetails
                {
                    Name = obj["Name"]?.ToString() ?? "Неизвестно",
                    Cores = obj["NumberOfCores"] != null ? int.Parse(obj["NumberOfCores"].ToString()) : 0,
                    Threads = obj["NumberOfLogicalProcessors"] != null ? int.Parse(obj["NumberOfLogicalProcessors"].ToString()) : 0
                };
            }
            return new ProcessorDetails { Name = "Неизвестно", Cores = 0, Threads = 0 };
        }
    }

    /// <summary>
    /// Класс, представляющий информацию о процессоре.
    /// </summary>
    public class ProcessorDetails
    {
        public string Name { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
    }
}
