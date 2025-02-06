namespace PcMonitorWebApi.Data.Models
{
    // Models/MemoryModule.cs
    public class MemoryModule
    {
        public int Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public int CapacityGB { get; set; }
    }
}
