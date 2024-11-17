using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly AppDbContext _context;
        public VehicleController(AppDbContext context) 
        { 
            _context = context;
        }  

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = await _context.Vehicles
                .Include(v=>v.Manufacturer)
                .Include(v=>v.Model)
                .Include (v=>v.VehicleCategory)
                .Include(v=>v.Manufacturer)
                .Select(v => new
                {
                    v.RegNumber,
                    v.Manufacturer.ManufacturerName,
                    v.Model.ModelName,
                    v.VehicleCategory.VehicleCategoryCode
                })
                .ToListAsync();


            return Ok(vehicles);
        }

        [HttpGet("{regNumb}")]
        public async Task<ActionResult<object>> GetVehicle(string regNumb)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.RegNumber == regNumb)
                .Select(v => new
                {
                    v.RegNumber,
                    ManufacturerName = v.Manufacturer.ManufacturerName,
                    ModelName = v.Model.ModelName,
                    VehicleCategoryCode = v.VehicleCategory.VehicleCategoryCode
                })
                .FirstOrDefaultAsync();

            return Ok(vehicle);
        }
    }
}
