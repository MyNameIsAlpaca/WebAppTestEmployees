using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTestEmployees.Models;

namespace WebAppTestEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttivitaDipendenteController : ControllerBase
    {
        private readonly DipendentiAziendaContext _context;

        public AttivitaDipendenteController(DipendentiAziendaContext context)
        {
            _context = context;
        }

        // GET: api/AttivitaDipendente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttivitaDipendente>>> GetAttivitaDipendentes()
        {
          if (_context.AttivitaDipendentes == null)
          {
              return NotFound();
          }
            return await _context.AttivitaDipendentes.ToListAsync();
        }

        // GET: api/AttivitaDipendente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttivitaDipendente>> GetAttivitaDipendente(int id)
        {
          if (_context.AttivitaDipendentes == null)
          {
              return NotFound();
          }
            var attivitaDipendente = await _context.AttivitaDipendentes.FindAsync(id);

            if (attivitaDipendente == null)
            {
                return NotFound();
            }

            return attivitaDipendente;
        }

        // PUT: api/AttivitaDipendente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttivitaDipendente(int id, AttivitaDipendente attivitaDipendente)
        {
            if (id != attivitaDipendente.Id)
            {
                return BadRequest();
            }

            _context.Entry(attivitaDipendente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttivitaDipendenteExists(id))
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

        // POST: api/AttivitaDipendente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttivitaDipendente>> PostAttivitaDipendente(AttivitaDipendente attivitaDipendente)
        {
          if (_context.AttivitaDipendentes == null)
          {
              return Problem("Entity set 'DipendentiAziendaContext.AttivitaDipendentes'  is null.");
          }
            _context.AttivitaDipendentes.Add(attivitaDipendente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttivitaDipendente", new { id = attivitaDipendente.Id }, attivitaDipendente);
        }

        // DELETE: api/AttivitaDipendente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttivitaDipendente(int id)
        {
            if (_context.AttivitaDipendentes == null)
            {
                return NotFound();
            }
            var attivitaDipendente = await _context.AttivitaDipendentes.FindAsync(id);
            if (attivitaDipendente == null)
            {
                return NotFound();
            }

            _context.AttivitaDipendentes.Remove(attivitaDipendente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttivitaDipendenteExists(int id)
        {
            return (_context.AttivitaDipendentes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
