namespace PcMonitorWebApi.Data.Models
{
    // Models/GraphicsCard.cs
    public class GraphicsCard
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int VRAMGB { get; set; }
    }
}
