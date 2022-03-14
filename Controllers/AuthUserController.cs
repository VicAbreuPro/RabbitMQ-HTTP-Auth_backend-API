using Microsoft.AspNetCore.Mvc;
using API_ES.Model;

namespace API_ES.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        [HttpPost("user")]
        public async Task<ActionResult> VerifyUser([FromForm] User user){
            
            await Task.Delay(2000);
            return Ok();
        }
    }
}
