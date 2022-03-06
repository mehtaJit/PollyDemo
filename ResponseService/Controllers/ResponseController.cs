using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        //GET /api/response/100
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetARespone(int id)
        {
            Random random = new Random();
            var rndInteger = random.Next(1, 101);

            if (rndInteger >= id)
            {
                Console.WriteLine("--> Falure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Console.WriteLine("--> Success - Generate a HTTP 200");
            return Ok();
        }
    }
}
