namespace PcMonitorWebApi.Data.Models
{
    // Models/Computer.cs
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public Processor Processor { get; set; }
        public ICollection<MemoryModule> MemoryModules { get; set; }
        public ICollection<DiskDrive> DiskDrives { get; set; }
        public ICollection<GraphicsCard> GraphicsCards { get; set; }
        public string OS { get; set; }
        public string SystemArchitecture { get; set; }
        public string WorkgroupOrDomain { get; set; }
        public string LastBootTime { get; set; }
        public ICollection<NetworkInterface> NetworkInterfaces { get; set; }
        public int? GroupId { get; set; } // Внешний ключ для связи с группой
        public Group? Group { get; set; }  // Навигационное свойство для группы
    }
}
