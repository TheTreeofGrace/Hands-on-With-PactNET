using Microsoft.AspNetCore.Mvc;
using SpiritAnimalBackend.Models;
using SpiritAnimalBackend.Repositories;

namespace SpiritAnimalBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpiritAnimalController : Controller
    {
        private IConfiguration Configuration { get; }
        private SpiritAnimalRepository _repository;

        public SpiritAnimalController(IConfiguration configuration)
        {
            Configuration = configuration;
            _repository = SpiritAnimalRepository.GetInstance();
        }

        // GET: api/SpiritAnimal
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SpiritAnimal>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSpiritAnimals()
        {
            var spiritAnimals = _repository.GetSpiritAnimals();
            if (spiritAnimals.Count == 0)
          {
              return new OkResult();
          }
            return new OkObjectResult(spiritAnimals);
        }

        // GET: api/SpiritAnimal/5
        [HttpGet("{long id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpiritAnimal))]
        public IActionResult GetSpiritAnimal(long id)
        {
            var spiritAnimal = _repository.GetSpiritAnimal(id);

            if (spiritAnimal == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(spiritAnimal);;
        }

        // PUT: api/SpiritAnimal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{long id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpiritAnimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutSpiritAnimal(long id, SpiritAnimal spiritAnimal)
        {
            if (id != spiritAnimal.Id)
            {
                return new BadRequestResult();
            }

            var existingAnimal = _repository.GetSpiritAnimal(id);
            if (existingAnimal == null)
            {
                return new NotFoundResult();
            }
            
            var result = _repository.PutSpiritAnimal(existingAnimal, spiritAnimal);

            return new OkObjectResult(result);
        }

        // POST: api/SpiritAnimal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SpiritAnimal))]
        public IActionResult PostSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            var animalExists = _repository.GetSpiritAnimal(spiritAnimal.Id);
            if (animalExists != null)
            {
                return new ConflictResult();
            }
            try
            {
                _repository.PostSpiritAnimal(spiritAnimal);
            }
            catch (Exception e)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new CreatedAtRouteResult(spiritAnimal.Id.ToString(), spiritAnimal);
        }

        // DELETE: api/SpiritAnimal/5
        [HttpDelete("{long id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteSpiritAnimal(long id)
        {
            var spiritAnimal = _repository.GetSpiritAnimal(id);
            if (spiritAnimal == null)
            {
                return new NotFoundResult();
            }

            _repository.DeleteSpiritAnimal(spiritAnimal);
            return new NoContentResult();
        }
        
    }
}
