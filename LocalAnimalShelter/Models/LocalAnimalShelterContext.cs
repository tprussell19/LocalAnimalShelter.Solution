using Microsoft.EntityFrameworkCore;

namespace LocalAnimalShelter.Models
{
  public class LocalAnimalShelterContext : DbContext
  {
    public LocalAnimalShelterContext(DbContextOptions<LocalAnimalShelterContext> options)
    : base(options)
    {
    }
    public DbSet<Animal> Animals { get; set; }
  }
}