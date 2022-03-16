using Microsoft.AspNetCore.Mvc;
using API_ES.Model;

namespace API_ES.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        [HttpPost("user")]
        public IActionResult VerifyUser([FromForm] User user){
            
            string resp = "deny";
            int aux = 0;

            if(user != null){
                try{
                    aux = Data.DataAccess.GetUser(user.UserName, user.Password);
                    if(aux != 0) resp = "allow";

                }catch(System.Exception){
                    return Unauthorized(resp);
                }
            }
            return Ok(resp);
        }

        [HttpPost("vhost")]
        public IActionResult Verifyhost([FromForm] Vhost host){

            string resp = "deny";
            int aux = 0;

            Console.WriteLine(host.vhost + host.UserName);
            if(host != null){
                try
                {
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

        [HttpPost("resource")]
        public IActionResult VerifyResource([FromForm]ResourceAuth resources){

            string resp = "deny";
            int aux = 0;
            
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                var val = HttpContext.Request.Form[key];
                Console.WriteLine(key + " | " + val);
            }
            //if(resource.UserName == null) test = "admin";

            if(resources != null){
                try
                {
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

        [HttpPost("topic")]
        public IActionResult VerifyTopic([FromForm] Topic topic){

            string resp = "deny";
            int aux = 0;

            //Console.WriteLine("Username:2"+ topic.UserName + "Topic Name:"+ topic.Name + "|Resource:" + topic.Resource + "|Permission:" + topic.Permission + "|Routing_Key:" + topic.RoutingKey);
            if(topic!= null){
                try
                {
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
