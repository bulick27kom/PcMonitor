﻿namespace PcMonitorWebApi.Data.Models
{
    // Models/Computer.cs
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public Processor Processor { get; set; }
        public List<MemoryModule> MemoryModules { get; set; }
        public List<DiskDrive> DiskDrives { get; set; }
        public List<GraphicsCard> GraphicsCards { get; set; }
        public string OS { get; set; }
        public string SystemArchitecture { get; set; }
        public string WorkgroupOrDomain { get; set; }
        public string LastBootTime { get; set; }
        public List<NetworkInterface> NetworkInterfaces { get; set; }
        public int? GroupId { get; set; } // Для группировки компьютеров
    }
}
