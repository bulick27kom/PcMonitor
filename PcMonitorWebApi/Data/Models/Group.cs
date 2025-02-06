namespace PcMonitorWebApi.Data.Models
{
    // Models/Group.cs
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Computer> Computers { get; set; } = new();
    }
}
