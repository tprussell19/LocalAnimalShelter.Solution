using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalAnimalShelter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LocalAnimalShelter.Controllers
{
  [Produces("application/json")]
  [ApiVersion("1.0")]
  [Route("api/v{version:ApiVersion}/[controller]")]
  [ApiController]
  public class AnimalsController : ControllerBase
  {
    private readonly LocalAnimalShelterContext _db;

    public AnimalsController(LocalAnimalShelterContext db)
    {
      _db = db;
    }

    private bool AnimalExists(int id) => _db.Animals.Any(a => a.AnimalId == id);

    /// <summary>
    /// Get a list of all of the animals available for adoption in the shelter
    /// </summary>
    /// <response code="200">Returns all animals from the database</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Animal>>> Get()
    => await _db.Animals.ToListAsync();

    /// <summary>
    /// Get the detailed information about one animal
    /// </summary>
    /// <response code="200">Returns the requested animal</response>
    /// <response code="404">Requested animal cannot be found in the database. Check that you have entered the correct AnimalId</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
      Animal a = await _db.Animals.FindAsync(id);
      if (a == null) return NotFound();
      return a;
    }

    /// <summary>
    /// Create the information for a new animal in the shelter
    /// </summary>
    /// <remarks>
    /// When adding a new animal to the database, please remove the "AnimalId" key-value pair from your entry. The database will assign an id to the entry upon successful creation.
    ///
    /// Sample request:
    /// ```
    /// {
    ///   "animalType": "Cat",
    ///   "name": "Felix",
    ///   "breed": "Domestic short hair",
    ///   "sex": "male",
    ///   "color": "Brown tabby",
    ///   "age": "3 years",
    ///   "weight": 15,
    ///   "description": "A true scaredy cat"
    /// }
    /// ```
    /// </remarks>
    /// <response code="201">Returns the new animal info</response>
    /// <response code="400">If the entry is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Animal>> Post(Animal a)
    {
      _db.Animals.Add(a);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetAnimal), new { id = a.AnimalId }, a);
    }

    /// <summary>
    /// Edit the information of an animal
    /// </summary>
    /// <remarks>
    /// Make sure that AnimalId matches the parameter id entered. Fill out all fields that need to be edited. If a field does not need to be edited, delete that key-value pair from the body of the query. Do not leave a trailing comma as that will result in a bad request.
    ///
    /// Sample request:
    /// ```
    /// {
    ///   "AnimalId": 2,
    ///   "age": "3 years, 2 months",
    ///   "weight": 14,
    ///   "description": "A true scaredy cat but also will wake you up at 3:45am asking for pets"
    /// }
    /// ```
    ///
    /// Sample Bad Request:
    /// ```
    /// {
    ///   "AnimalId": 2,
    ///   "Name": "Felix",
    /// }
    /// ```
    /// </remarks>
    /// <response code="200">Successful request. Returns the edited object.</response>
    /// <response code="400">Bad request. Check to make sure an object exists with the id you have entered.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Animal>> Put(int id, Animal a)
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
      return a;
    }

    /// <summary>
    /// Delete the entry for an animal that has been adopted
    /// </summary>
    /// <response code="204">Successful request. The entry has been deleted.</response>
    /// <response code="404">Not found. Check to make sure an object exists with the id you have entered.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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