namespace PcMonitorWebApi.Data.Models
{
    // Models/NetworkInterface.cs
    public class NetworkInterface
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MACAddress { get; set; } = string.Empty;
        public ICollection<string> IPAddresses { get; set; } = new List<string>();
        public ICollection<string> SubnetMasks { get; set; } = new List<string>();
    }
}
