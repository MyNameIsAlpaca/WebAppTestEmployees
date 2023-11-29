using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAppTestEmployees.Blogic.Authentication;
using WebAppTestEmployees.Models;

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
            using (var authorizationDB = new DipendentiAziendaContext())
            {
                    var utente = authorizationDB.myUser.FromSqlRaw($"select * from [dbo].[myUser] where email = @email and password = @password", new SqlParameter("@email", user.Email), new SqlParameter("@password", user.Password)).SingleOrDefault();

                    if (utente != null) { 
                
                        return Ok();
                    } else
                    {
                    return BadRequest();
                    }
                }
            }
        }

        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
    }

    