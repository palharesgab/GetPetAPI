using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {

        public readonly DataContext _context;

        public PetController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pet>>> GetPet()
        {
            return Ok(await _context.Pets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if(pet == null)
            {
                return BadRequest("Pet not found.");
            }
            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pet>>> AddPet(Pet pet)
        {
            _context.Pets.Add(pet);

            await _context.SaveChangesAsync();
            return Ok(await _context.Pets.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Pet>>> UpdatePet(Pet request)
        {
            var dbPet = await _context.Pets.FindAsync(request.Id);

            if (dbPet == null)
            {
                return BadRequest("Pet not found.");
            }

            dbPet.Name = request.Name;
            dbPet.Place = request.Place;
            dbPet.Description = request.Description;
            dbPet.CreationDate = request.CreationDate;
            dbPet.ClosedDate = request.ClosedDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.Pets.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Pet>>> DeletePet(int id)
        {
            var dbPet = await _context.Pets.FindAsync(id);
            if (dbPet == null)
            {
                return BadRequest("Pet not found.");
            }

            _context.Pets.Remove(dbPet);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pets.ToListAsync());
        }
    }
}
