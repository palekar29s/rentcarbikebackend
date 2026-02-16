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


        //[HttpPost("add-Vehicle")]
        //public IActionResult InsertVehicleWithLogin([FromBody] VehicleClass vehicle)
        //{
        //    if (vehicle == null)
        //    {
        //        return BadRequest("Vehicle data is required.");
        //    }

        //    try
        //    {
        //        _database.InsertVehicle(vehicle);
        //        return Ok("Vehicle and login inserted successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception (if logging is set up)
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPost("add-vehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] vehicleeClass vehicle)
        {
            if (vehicle == null)
                return BadRequest("Vehicle data required");

            try
            {
                int vehicleId = await _database.InsertVehicle(vehicle);
                return Ok(new { VehicleId = vehicleId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //insert imageees in the database and also save in the folder

        [HttpPost("upload-vehicle-images")]
        public async Task<IActionResult> UploadVehicleImages(
    [FromForm] int vehicleId,
    [FromForm] List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
                return BadRequest("No images provided");

            try
            {
                await _database.InsertVehicleImages(vehicleId, images);
                return Ok(new { message = "Images uploaded successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // ------------------------------------------------------
        //[HttpPost("insert-the-images")]
        //public IActionResult InsertImage([FromBody] VehicleImagesClass image)
        //{
        //    if (image == null)
        //        return BadRequest("Image data is required.");

        //    _database.InsertVehicleImage(image);
        //    return Ok("Image inserted successfully.");
        //}

        

       
       

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

        //this for the update of unavlbel
        // ------------------------------------------------------
        [HttpPut("set-unavailable/{vehicleId}")]
        public IActionResult SetVehicleUnavailable(int vehicleId)
        {
            if (vehicleId <= 0)
                return BadRequest(new { message = "Invalid Vehicle Id." });

            _database.UpdateVehicleStatus(vehicleId);

            return Ok(new { message = "Vehicle status updated successfully." });
        }
        //[HttpPost("upload-image")]
        //public async Task<IActionResult> UploadImage(IFormFile file, string imageName)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file selected");

        //    if (string.IsNullOrEmpty(imageName))
        //        return BadRequest("Image name required");

        //    // Get extension automatically (.jpg / .png)
        //    string extension = Path.GetExtension(file.FileName).ToLower();

        //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        //    if (!allowedExtensions.Contains(extension))
        //        return BadRequest("Only JPG and PNG allowed");

        //    string angularPath = @"C:\Users\Sandesh Palekar\myproject\carbikerental\src\assets";

        //    if (!Directory.Exists(angularPath))
        //        Directory.CreateDirectory(angularPath);

        //    // Combine user name + extension
        //    string finalFileName = imageName + extension;

        //    string fullPath = Path.Combine(angularPath, finalFileName);

        //    using (var stream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return Ok(new
        //    {
        //        imagePath = "assets/" + finalFileName
        //    });
        //}




    }
}
