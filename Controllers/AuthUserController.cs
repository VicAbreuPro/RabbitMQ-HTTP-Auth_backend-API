using Microsoft.AspNetCore.Mvc;
using API_ES.Model;

namespace API_ES.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        // Verify existing user in database
        [HttpPost("user")]
        public IActionResult VerifyUser([FromForm] User user){
            
            string resp = "deny";
            int aux = 0;

            if(user != null){
                try{
                    // Auxiliary function to access database
                    aux = Data.DataAccess.GetUser(user.UserName, user.Password);
                    if(aux != 0) resp = "allow";

                }catch(System.Exception){
                    return Unauthorized(resp);
                }
            }
            return Ok(resp);
        }

        // Verify access to virtual host
        [HttpPost("vhost")]
        public IActionResult Verifyhost([FromForm] Vhost host){

            string resp = "deny";
            int aux = 0;

            if(host != null){
                try
                {
                    // Auxiliary function to access database
                    aux = Data.DataAccess.GetVhost(host.vhost);
                    if(aux != 0) resp = "allow";
                }
                catch (System.Exception)
                {
                    return Unauthorized(resp);
                }
            }
            return Ok(resp);
        }

        // Verify resources properties in virtual host
        [HttpPost("resource")]
        public IActionResult VerifyResource([FromForm]ResourceAuth resources){

            string resp = "deny";
            int aux = 0;
            
            /*//Test input field in forms
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                var val = HttpContext.Request.Form[key];
                Console.WriteLine(key + " | " + val);
            }*/

            if(resources != null){
                try
                {
                    // Auxiliary function to access database
                    aux = Data.DataAccess.GetResource(resources.UserName, resources.Resource.ToString(), resources.Permission.ToString());
                    if(aux != 0) resp = "allow";
                }
                catch (System.Exception)
                {
                   return Unauthorized(resp);
                }
            }
            return Ok(resp);
        }

        // Veirfy access to requested topic
        [HttpPost("topic")]
        public IActionResult VerifyTopic([FromForm] Topic topic){

            string resp = "deny";
            int aux = 0;

            if(topic!= null){
                try
                {
                    // Auxiliary function to access database
                    aux = Data.DataAccess.GetTopic(topic.UserName, topic.Name, topic.Permission.ToString(), topic.RoutingKey);
                    if(aux != 0) resp = "allow";
                }
                catch (System.Exception)
                {
                    return Unauthorized(resp);
                }
            }
            return Ok(resp);
        }
    }
}
