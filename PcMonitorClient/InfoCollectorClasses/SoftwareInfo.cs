// SoftwareInfo.cs
using System;
using System.Management;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для сбора информации о программной части ПК.
    /// </summary>
    public class SoftwareInfo
    {
        /// <summary>
        /// Получает название операционной системы.
        /// </summary>
        /// <returns>Строка с названием ОС.</returns>
        public string GetOSInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Caption"]?.ToString() ?? "Неизвестно";
            }
            return "Неизвестно";
        }

        /// <summary>
        /// Получает архитектуру системы (32/64 бит).
        /// </summary>
        /// <returns>Строка "32-bit" или "64-bit".</returns>
        public string GetSystemArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
        }

        /// <summary>
        /// Получает рабочую группу или домен.
        /// </summary>
        /// <returns>Строка с именем рабочей группы или домена.</returns>
        public string GetWorkgroupOrDomain()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Domain FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Domain"]?.ToString() ?? "Неизвестно";
            }
            return "Неизвестно";
        }

        /// <summary>
        /// Получает дату и время последней загрузки ПК.
        /// </summary>
        /// <returns>Строка в формате "дд.мм.гггг чч:мм".</returns>
        public string GetLastBootTime()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj["LastBootUpTime"] != null)
                {
                    string rawTime = obj["LastBootUpTime"].ToString();
                    if (DateTime.TryParseExact(rawTime.Substring(0, 14), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime bootTime))
                    {
                        return bootTime.ToString("dd.MM.yyyy HH:mm");
                    }
                }
            }
            return "Неизвестно";
        }

        /// <summary>
        /// Получает имя компьютера.
        /// </summary>
        /// <returns>Имя ПК.</returns>
        public string GetComputerName()
        {
            return Environment.MachineName;
        }
    }
}
