using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcMonitorWebApi.Data.Models;
using PcMonitorWebApi.Data;

namespace PcMonitorWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComputersController> _logger;

        public ComputersController(AppDbContext context, ILogger<ComputersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostComputer([FromBody] Computer computer)
        {
            try
            {
                _context.Computers.Add(computer);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetComputer), new { id = computer.Id }, computer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving computer data.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return Ok(computer);
        }
    }

}
