namespace PcMonitorWebApi.Data.Models
{
    // Models/DiskDrive.cs
    public class DiskDrive
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int CapacityGB { get; set; }
    }
}
