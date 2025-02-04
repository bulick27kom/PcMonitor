// GraphicsInfo.cs
using System.Collections.Generic;
using System.Management;

namespace PcMonitorClient
{
    /// <summary>
    /// Класс для получения информации о видеокартах с использованием WMI.
    /// </summary>
    public class GraphicsInfo
    {
        /// <summary>
        /// Получить список всех видеокарт.
        /// </summary>
        /// <returns>Список объектов с данными о видеокартах.</returns>
        public List<GraphicsCard> GetGraphicsInfo()
        {
            List<GraphicsCard> graphicsCards = new List<GraphicsCard>();

            // WMI-запрос для получения данных о видеокартах
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                // Получаем имя видеокарты
                string name = obj["Name"]?.ToString() ?? "Неизвестно";

                // Проверяем, есть ли информация о видеопамяти
                int vramGB = 0;
                if (obj["AdapterRAM"] != null)
                {
                    object ramValue = obj["AdapterRAM"];

                    // Приведение к ulong, если значение UInt32 или UInt64
                    if (ramValue is uint ramUInt32)
                    {
                        vramGB = (int)(ramUInt32 / (1024 * 1024 * 1024));
                    }
                    else if (ramValue is ulong ramUInt64)
                    {
                        vramGB = (int)(ramUInt64 / (1024 * 1024 * 1024));
                    }
                }

                graphicsCards.Add(new GraphicsCard
                {
                    Name = name,
                    VRAMGB = vramGB
                });
            }

            return graphicsCards;
        }
    }

    /// <summary>
    /// Класс, представляющий видеокарту.
    /// </summary>
    public class GraphicsCard
    {
        public string Name { get; set; }
        public int VRAMGB { get; set; }
    }
}
