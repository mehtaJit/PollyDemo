using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //GET api/request
        [HttpGet]
        public async Task<IActionResult> MakeRequest()
        {
            var client = _httpClientFactory.CreateClient("Test");

            var response = await client.GetAsync("https://localhost:7206/api/response/25");   

            #region Polly Code
            //This code is moved to Program.cs to make it more centralized

            //var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(               //Calling with Polly using Immediate Retry
            //    () => client.GetAsync("https://localhost:7206/api/response/25"));

            //var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(                  //Calling with Polly using Linear Retry
            //    () => client.GetAsync("https://localhost:7206/api/response/25"));


            //var response = await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(               //Calling with Polly using Exponential Retry
            //    () => client.GetAsync("https://localhost:7206/api/response/25"));
            #endregion


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> ResponseService returned SUCCESS");
                return Ok();
            }

            Console.WriteLine("--> ResponseService returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
