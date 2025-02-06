namespace PcMonitorWebApi.Data.Models
{
    // Models/NetworkInterface.cs
    public class NetworkInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MACAddress { get; set; }
        public List<string> IPAddresses { get; set; }
        public List<string> SubnetMasks { get; set; }
    }
}
