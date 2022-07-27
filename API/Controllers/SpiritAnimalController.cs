using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaygroundAPI6Cont.Models;

namespace PlaygroundAPI6Cont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiritAnimalController : ControllerBase
    {
        private readonly SpiritAnimalContext _context;

        public SpiritAnimalController(SpiritAnimalContext context)
        {
            _context = context;
        }

        // GET: api/SpiritAnimal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpiritAnimal>>> GetSpiritAnimals()
        {
          if (_context.SpiritAnimals == null)
          {
              return NotFound();
          }
            return await _context.SpiritAnimals.ToListAsync();
        }

        // GET: api/SpiritAnimal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpiritAnimal>> GetSpiritAnimal(long id)
        {
          if (_context.SpiritAnimals == null)
          {
              return NotFound();
          }
            var spiritAnimal = await _context.SpiritAnimals.FindAsync(id);

            if (spiritAnimal == null)
            {
                return NotFound();
            }

            return spiritAnimal;
        }

        // PUT: api/SpiritAnimal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPut("{id}")]
        public async Task<ActionResult<SpiritAnimal>> PutSpiritAnimal(long id, SpiritAnimal spiritAnimal)
        {
            if (id != spiritAnimal.Id)
            {
                return BadRequest();
            }

            _context.Entry(spiritAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpiritAnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return spiritAnimal;
        }

        // POST: api/SpiritAnimal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPost]
        public async Task<ActionResult<SpiritAnimal>> PostSpiritAnimal(SpiritAnimal spiritAnimal)
        {
          if (_context.SpiritAnimals == null)
          {
              return Problem("Entity set 'SpiritAnimalContext.SpiritAnimals'  is null.");
          }
            _context.SpiritAnimals.Add(spiritAnimal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSpiritAnimal), new { id = spiritAnimal.Id }, spiritAnimal);
        }

        // DELETE: api/SpiritAnimal/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpiritAnimal(long id)
        {
            if (_context.SpiritAnimals == null)
            {
                return NotFound();
            }
            var spiritAnimal = await _context.SpiritAnimals.FindAsync(id);
            if (spiritAnimal == null)
            {
                return NotFound();
            }

            _context.SpiritAnimals.Remove(spiritAnimal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpiritAnimalExists(long id)
        {
            return (_context.SpiritAnimals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
