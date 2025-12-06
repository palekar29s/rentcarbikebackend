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

        [HttpGet("gets-the-images")]
        public IActionResult GetAllImages()
        {
            var images = _database.GetImages();
            return Ok(images);
        }


        // ------------------------------------------------------
        [HttpPost("insert-the-images")]
        public IActionResult InsertImage([FromBody] VehicleImagesClass image)
        {
            if (image == null)
                return BadRequest("Image data is required.");

            _database.InsertVehicleImage(image);
            return Ok("Image inserted successfully.");
        }

        

        [HttpGet("{vehicleId}")]
        public IActionResult GetImagesByVehicleId(int vehicleId)
        {
            if (vehicleId <= 0)
                return BadRequest("Invalid VehicleId");

            List<VehicleImagesClass> images = _database.GetVehicleImagesById(vehicleId);

            return Ok(images);
        }

        [HttpGet("getvehicleswithimages")]
        public IActionResult GetVehiclesWithImages()
        {
            try
            {
                var vehicles = _database.GetVehiclesWithImages();

                if (vehicles == null || vehicles.Count == 0)
                {
                    return NotFound(new { message = "No vehicles found" });
                }

                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }
        [HttpGet("GetVehicleById/{id}")]
        public IActionResult GetVehicleById(int id)
        {
            var result = _database.GetVehicleById(id);
            if (result == null)
                return NotFound("Vehicle not found");

            return Ok(result);
        }

    }
}
