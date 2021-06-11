using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalAnimalShelter.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace LocalAnimalShelter.Controllers
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:ApiVersion}/[controller]")]
  public class AnimalsController : ControllerBase
  {
    private readonly LocalAnimalShelterContext _db;

    public AnimalsController(LocalAnimalShelterContext db)
    {
      _db = db;
    }

    private bool AnimalExists(int id) => _db.Animals.Any(a => a.AnimalId == id);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> Get()
    => await _db.Animals.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
      Animal a = await _db.Animals.FindAsync(id);
      if (a == null) return NotFound();
      return a;
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal a)
    {
      _db.Animals.Add(a);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetAnimal), new { id = a.AnimalId }, a);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Animal a)
    {
      if (id != a.AnimalId) return BadRequest();

      _db.Entry(a).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnimalExists(id)) return NotFound();
        else throw;
      }
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnimal(int id)
    {
      Animal a = await _db.Animals.FindAsync(id);
      if (a == null) return NotFound();

      _db.Animals.Remove(a);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}