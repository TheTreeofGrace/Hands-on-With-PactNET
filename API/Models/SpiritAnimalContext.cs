using Microsoft.EntityFrameworkCore;

namespace PlaygroundAPI6Cont.Models
{
    public class SpiritAnimalContext:DbContext
    {
        public SpiritAnimalContext(DbContextOptions<SpiritAnimalContext> options)
            :base(options)
        {
        }
        public DbSet<SpiritAnimal> SpiritAnimals { get; set; } = null!;
    }
}

