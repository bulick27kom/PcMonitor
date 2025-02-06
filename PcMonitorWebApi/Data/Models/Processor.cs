namespace PcMonitorWebApi.Data.Models
{
    // Models/Processor.cs
    public class Processor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
    }
}
