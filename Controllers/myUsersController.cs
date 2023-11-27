using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTestEmployees.Models;
using WebAppTestEmployees.Blogic.Authentication;


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
        [HttpGet("{id}")]
        public async Task<ActionResult<myUser>> GetmyUser(int id)
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
            _context.myUser.Add(myUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmyUser", new { id = myUser.Id }, myUser);
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
