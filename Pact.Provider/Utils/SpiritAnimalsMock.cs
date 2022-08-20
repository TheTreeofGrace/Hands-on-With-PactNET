using SpiritAnimalBackend.Models;

namespace Pact.Provider.Utils;

public class SpiritAnimalsMock
{
    public static readonly SpiritAnimal Panda = new (10, "Black and White", "Panda");
    public static readonly SpiritAnimal Unicorn = new (1, "Unicorn", "White");
    public static readonly SpiritAnimal Dog = new (2, "Dog", "Brown");
}