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

        


        // ------------------------------------------------------
        [HttpPost("insert-the-images")]
        public IActionResult InsertImage([FromBody] VehicleImagesClass image)
        {
            if (image == null)
                return BadRequest("Image data is required.");

            _database.InsertVehicleImage(image);
            return Ok("Image inserted successfully.");
        }

        

       
       

        //this is use in the get partiuclar vechile in detail 
        [HttpGet("GetVehicleById/{id}")]
        public IActionResult GetVehicleById(int id)
        {
            var result = _database.GetVehicleById(id);
            if (result == null)
                return NotFound("Vehicle not found");

            return Ok(result);
        }

        // this is the filter in the search section it is use 

        [HttpGet("getvehicleswithimagess")]
        public IActionResult GetVehiclesWithImagess(
    string? vehicle,
    DateTime? checkin,
    DateTime? checkout,
    string? location,
    int? seats)
        {
            var result = _database.GetVehiclesWithImagess(
                vehicle,
                checkin,
                checkout,
                location,
                seats
            );

            return Ok(result);
        }

    }
}
