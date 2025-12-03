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

        
    
    }
}
