using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentcarbike.Models;

namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly Database _database;
        public VehicleController(Database database)
        {
            _database = database;
        }

        //all endpoint of vehicle table 
        [HttpGet("get-all-Vehicle")]
        public ActionResult<List<VehicleClass>> GetVehicles()
        {
            var result = _database.GetVehicles();
            return Ok(result);
        }

        [HttpPost("add-Vehicle")]
        public IActionResult InsertEmployeeWithLogin([FromBody] VehicleClass vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Employee data is required.");
            }

            try
            {
                _database.InsertVehicle(vehicle);
                return Ok("Employee and login inserted successfully.");
            }
            catch (Exception ex)
            {
                // Log exception (if logging is set up)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
