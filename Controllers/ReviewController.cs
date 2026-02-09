using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentcarbike.Models;

namespace rentcarbike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly Database _database;
        public ReviewController(Database database)
        {
            _database = database;
        }
    }
}
