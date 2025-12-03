using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
         private readonly string _connectionString;


        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }
        [HttpGet("get-all-the-user-deatils")]
        public ActionResult<List<UsersClass>> GetAllSignups()
        {
            var result = _database.GetAllSignups();
            return Ok(result);
        }
        [HttpGet("get-username-password")]
        public ActionResult<List<UsersClass>> GetAllUserPass()
        {
            var result = _database.GetAllUserPass();
            return Ok(result);
        }

        [HttpPost("insert-the-user-deatils")]
        public IActionResult Insertinuser([FromBody] UsersClass usersclass)
        {
            try
            {
                _database.InsertSignup(signup);
                return Ok("insert the data");
            }
            catch (Exception ex)
            {
                // Log exception (if logging is set up)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
