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
        [HttpGet("get-all-booking")]
        public ActionResult<List<BookingClass>> GetAll()
        {
            var result = _database.GetBookingClasses();
            return Ok(result);
        }

        // POST: api/booking/add-booking
        [HttpPost("add-booking")]
        public IActionResult AddBooking([FromBody] BookingClass booking)
        {
            if (booking == null)
                return BadRequest("Booking data is required");

            booking.BookingId = null;

            int bookingId = _database.AddBooking(booking);

            if (bookingId > 0)
                return Ok(new { bookingId = bookingId });

            return StatusCode(500, "Failed to create booking");
        }


        [HttpPut("update-booking-status/{bookingId}")]
        public IActionResult UpdateBookingStatus(int bookingId)
        {
            bool result = _database.UpdateBookingStatus(bookingId, "Payment Done");

            if (result)
                return Ok(new { message = "Payment status updated successfully" });

            return StatusCode(500, "Failed to update booking status");
        }
        [HttpGet("booking-history/{userId}")]
        public IActionResult GetBookingHistoryByUser(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid UserId");

            var result = _database.GetBookingsByUserId(userId);
            return Ok(result);
        }

    }
}