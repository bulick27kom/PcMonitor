using PcMonitorWebApi.Data.Models;

namespace PcMonitorWebApi.Servises.Interfaces
{
    public interface IComputerService
    {
        Task<Computer?> GetComputerByNameAsync(string name);
        Task<IEnumerable<Computer>> GetAllComputersAsync();
        Task<Computer> AddComputeAsync(Computer computer);
        Task<Computer> UpdateComputeAsync(Computer computer);
    }
}
