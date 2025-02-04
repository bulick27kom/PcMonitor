// NetworkInfo.cs
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для сбора информации о сетевых интерфейсах.
    /// </summary>
    public class NetworkInfo
    {
        /// <summary>
        /// Получает информацию о всех сетевых интерфейсах.
        /// </summary>
        /// <returns>Список объектов NetworkInterfaceDetails.</returns>
        public List<NetworkInterfaceDetails> GetNetworkInfo()
        {
            List<NetworkInterfaceDetails> networkInterfaces = new List<NetworkInterfaceDetails>();

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up) // Фильтруем только активные интерфейсы
                {
                    NetworkInterfaceDetails details = new NetworkInterfaceDetails
                    {
                        Name = nic.Name,
                        MACAddress = nic.GetPhysicalAddress().ToString(),
                        IPAddresses = new List<string>(),
                        SubnetMasks = new List<string>()
                    };

                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4 только
                        {
                            details.IPAddresses.Add(ip.Address.ToString());
                            details.SubnetMasks.Add(ip.IPv4Mask.ToString());
                        }
                    }

                    networkInterfaces.Add(details);
                }
            }

            return networkInterfaces;
        }
    }

    /// <summary>
    /// Класс для хранения информации о сетевом интерфейсе.
    /// </summary>
    public class NetworkInterfaceDetails
    {
        public string Name { get; set; }
        public string MACAddress { get; set; }
        public List<string> IPAddresses { get; set; }
        public List<string> SubnetMasks { get; set; }
    }
}
