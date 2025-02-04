// MemoryInfo.cs
using System.Collections.Generic;
using System.Management;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для получения информации о модулях оперативной памяти с использованием WMI.
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// Получить список информации о каждом модуле оперативной памяти.
        /// </summary>
        /// <returns>Список объектов с данными о модулях памяти.</returns>
        public List<MemoryModule> GetMemoryInfo()
        {
            List<MemoryModule> memoryModules = new List<MemoryModule>();

            // WMI-запрос для получения данных о всех модулях памяти
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                ulong capacity = (ulong)obj["Capacity"];
                ulong capacityInGB = capacity / (1024 * 1024 * 1024);

                memoryModules.Add(new MemoryModule
                {
                    PartNumber = obj["PartNumber"]?.ToString() ?? "Неизвестно",
                    CapacityGB = (int)capacityInGB
                });
            }

            return memoryModules;
        }
    }

    /// <summary>
    /// Класс, представляющий отдельный модуль памяти.
    /// </summary>
    public class MemoryModule
    {
        public string PartNumber { get; set; }
        public int CapacityGB { get; set; }
    }
}
