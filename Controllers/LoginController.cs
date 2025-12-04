using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentcarbike.Models;


namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly Database _database;

        public LoginController(Database database)
        {
            _database = database;
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
                _database.InsertSignup(usersclass);
                return Ok("insert the data");
            }
            catch (Exception ex)
            {
                // Log exception (if logging is set up)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
