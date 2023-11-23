using Microsoft.AspNetCore.Mvc;
using WebAppTestEmployees.Blogic.Authentication;

namespace WebAppTestEmployees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [BasicAutorizationAttributes]
        [HttpPost]
        public IActionResult Auth(User user)
        {
            return Ok();
        }

        public class User
        {
            public string Name { get; set; }
            public string Password { get; set; }

        }
    }
}
    