using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTestEmployees.Blogic.Authentication;
using WebAppTestEmployees.Models;

namespace WebAppTestEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagraficaGenericaController : ControllerBase
    {
        private readonly DipendentiAziendaContext _context;

        public AnagraficaGenericaController(DipendentiAziendaContext context)
        {
            _context = context;
        }

        // GET: api/AnagraficaGenerica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnagraficaGenerica>>> GetAnagraficaGenericas()
        {
          if (_context.AnagraficaGenericas == null)
          {
              return NotFound();
          }
            return await _context.AnagraficaGenericas.Include(emp => emp.AttivitaDipendentes).ToListAsync();
        }

        // GET: api/AnagraficaGenerica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnagraficaGenerica>> GetAnagraficaGenerica(string id)
        {
          if (_context.AnagraficaGenericas == null)
          {
              return NotFound();
          }
            //var anagraficaGenerica = await _context.AnagraficaGenericas.FindAsync(id);

            var anagraficaGenerica = await _context.AnagraficaGenericas.Include(i => i.AttivitaDipendentes).FirstOrDefaultAsync(i => i.Matricola == id);

            if (anagraficaGenerica == null)
            {
                return NotFound();
            }

            return anagraficaGenerica;
        }

        // PUT: api/AnagraficaGenerica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnagraficaGenerica(string id, AnagraficaGenerica anagraficaGenerica)
        {
            if (id != anagraficaGenerica.Matricola)
            {
                return BadRequest();
            }

            _context.Entry(anagraficaGenerica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnagraficaGenericaExists(id))
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

        // POST: api/AnagraficaGenerica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [BasicAutorizationAttributes]

        public async Task<ActionResult<AnagraficaGenerica>> PostAnagraficaGenerica(AnagraficaGenerica anagraficaGenerica)
        {
          if (_context.AnagraficaGenericas == null)
          {
              return Problem("Entity set 'DipendentiAziendaContext.AnagraficaGenericas'  is null.");
          }
            _context.AnagraficaGenericas.Add(anagraficaGenerica);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnagraficaGenericaExists(anagraficaGenerica.Matricola))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnagraficaGenerica", new { id = anagraficaGenerica.Matricola }, anagraficaGenerica);
        }

        // DELETE: api/AnagraficaGenerica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnagraficaGenerica(string id)
        {
            if (_context.AnagraficaGenericas == null)
            {
                return NotFound();
            }
            var anagraficaGenerica = await _context.AnagraficaGenericas.FindAsync(id);
            if (anagraficaGenerica == null)
            {
                return NotFound();
            }

            _context.AnagraficaGenericas.Remove(anagraficaGenerica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnagraficaGenericaExists(string id)
        {
            return (_context.AnagraficaGenericas?.Any(e => e.Matricola == id)).GetValueOrDefault();
        }
    }
}
