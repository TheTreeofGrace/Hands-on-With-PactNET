using Microsoft.AspNetCore.Mvc;
using SpiritAnimalBackend.Models;
using SpiritAnimalBackend.Repositories;

namespace SpiritAnimalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiritAnimalController : Controller
    {
        private IConfiguration Configuration { get; }
        private SpritAnimalRepository _repository;

        public SpiritAnimalController(IConfiguration configuration)
        {
            Configuration = configuration;
            _repository = SpritAnimalRepository.GetInstance();
        }

        // GET: api/SpiritAnimal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpiritAnimal>>> GetSpiritAnimals()
        {
            var spiritAnimals = _repository.GetSpiritAnimals();
            if (spiritAnimals.Count == 0)
          {
              return NoContent();
          }
            return spiritAnimals;
        }

        // GET: api/SpiritAnimal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpiritAnimal>> GetSpiritAnimal(long id)
        {
            var spiritAnimal = _repository.GetSpiritAnimal(id);

            if (spiritAnimal == null)
            {
                return NotFound("Requested spirit animal does not exist.");
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
                return BadRequest("ID and spirit animal id must match");
            }

            var existingAnimal = _repository.GetSpiritAnimal(id);
            if (existingAnimal == null)
            {
                return NotFound("Please use POST request to create spirit animal.");
            }

            return _repository.PutSpiritAnimal(existingAnimal, spiritAnimal);
        }

        // POST: api/SpiritAnimal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPost]
        public async Task<ActionResult<SpiritAnimal>> PostSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            try
            {
                _repository.PostSpiritAnimal(spiritAnimal);
            }
            catch (Exception e)
            {
                return BadRequest("Internal Server Error. " + e.Message);
            }

            return CreatedAtAction(nameof(GetSpiritAnimal), new { id = spiritAnimal.Id }, spiritAnimal);
        }

        // DELETE: api/SpiritAnimal/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpiritAnimal(long id)
        {
            var spiritAnimal = _repository.GetSpiritAnimal(id);
            if (spiritAnimal == null)
            {
                return NotFound("Requested spirit animal to remove does not exist.");
            }

            _repository.DeleteSpiritAnimal(spiritAnimal);
            return NoContent();
        }
        
    }
}
