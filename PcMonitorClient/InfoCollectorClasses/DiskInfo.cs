// DiskInfo.cs
using System.Collections.Generic;
using System.Management;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для получения информации о жестких дисках и SSD с использованием WMI.
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// Получить список всех жестких дисков и SSD.
        /// </summary>
        /// <returns>Список объектов с данными о дисках.</returns>
        public List<DiskDrive> GetDiskInfo()
        {
            List<DiskDrive> diskDrives = new List<DiskDrive>();

            // WMI-запрос для получения данных о дисках
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject obj in searcher.Get())
            {
                ulong size = obj["Size"] != null ? (ulong)obj["Size"] / (1024 * 1024 * 1024) : 0;

                diskDrives.Add(new DiskDrive
                {
                    Model = obj["Model"]?.ToString() ?? "Неизвестно",
                    CapacityGB = (int)size
                });
            }

            return diskDrives;
        }
    }

    /// <summary>
    /// Класс, представляющий жесткий диск или SSD.
    /// </summary>
    public class DiskDrive
    {
        public string Model { get; set; }
        public int CapacityGB { get; set; }
    }
}
