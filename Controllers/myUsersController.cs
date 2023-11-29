using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTestEmployees.Models;
using WebAppTestEmployees.Blogic.Authentication;
using static WebAppTestEmployees.Controllers.LoginController;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.InteropServices;

namespace WebAppTestEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class myUsersController : ControllerBase
    {
        private readonly DipendentiAziendaContext _context;

        public myUsersController(DipendentiAziendaContext context)
        {
            _context = context;
        }

        // GET: api/myUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<myUser>>> GetUser()
        {
          if (_context.myUser == null)
          {
              return NotFound();
          }
            return await _context.myUser.ToListAsync();
        }

        // GET: api/myUsers/5
        [HttpGet("{email}")]
        public async Task<ActionResult<myUser>> GetMyUser(string email)
        {
            if (_context.myUser == null)
            {
                return NotFound();
            }
            var myUser = await _context.myUser.Where(e => e.email == email).FirstOrDefaultAsync();

            if (myUser == null)
            {
                return NotFound();
            }

            return myUser;
        }

        // PUT: api/myUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmyUser(int id, myUser myUser)
        {
            if (id != myUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(myUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!myUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/myUsers

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [BasicAutorizationAttributes]
        [HttpPost]

        public async Task<ActionResult<myUser>> PostmyUser(myUser myUser)
        {
            if (_context.myUser == null)
            {
                return Problem("Entity set 'DipendentiAziendaContext.User'  is null.");
            }

            KeyValuePair<string,string> hashpass = EncryptSaltString(myUser.password);

            myUser.password = hashpass.Value;

            myUser.salt = hashpass.Key;

            _context.myUser.Add(myUser);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmyUser", new { email = myUser.email }, myUser);
        }
        private KeyValuePair<string, string> EncryptSaltString(string pwdNeedToHash)
        {
            byte[] byteSalt = new byte[16];
            string EncResult = string.Empty;
            string EncSalt = string.Empty;
            try
            {
                RandomNumberGenerator.Fill(byteSalt);
                EncResult = Convert.ToBase64String(
                    //dotnet add package Microsoft.AspNetCore.Cryptography.KeyDerivation --version 7.0.12
                    KeyDerivation.Pbkdf2(
                        password: pwdNeedToHash,
                        salt: byteSalt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 132
                    )
                );
                EncSalt = Convert.ToBase64String(byteSalt);
            }
            catch (System.Exception)
            {
                throw;
            }

            return new KeyValuePair<string, string>(EncSalt, EncResult);
        }

        // DELETE: api/myUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemyUser(int id)
        {
            if (_context.myUser == null)
            {
                return NotFound();
            }
            var myUser = await _context.myUser.FindAsync(id);
            if (myUser == null)
            {
                return NotFound();
            }

            _context.myUser.Remove(myUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool myUserExists(int id)
        {
            return (_context.myUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
