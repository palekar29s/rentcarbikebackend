using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentcarbike.Models;

namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly Database _database;
        public ContactController(Database database)
        {
            _database = database;
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            _database.AddContact(contact.Name, contact.Email, contact.Phone, contact.Message);

            return Ok(new { message = "Message submitted successfully" });
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _database.GetAllContacts();
            return Ok(contacts);
        }

    }
}
