using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentcarbike.Models;

namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {


        private readonly Database _database;

        public BookingController(Database database)
        {
            _database = database;
        }


        //all the endpoint booking Table 
        [HttpGet("get-all-booking ")]
        public ActionResult<List<BookingClass>> GetAll()
        {
            var result = _database.GetBookingClasses();
            return Ok(result);
        }


    }
}
