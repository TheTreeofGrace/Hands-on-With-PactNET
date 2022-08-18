using SpiritAnimalBackend.Controllers;
using SpiritAnimalBackend.Models;

namespace SpiritAnimalBackend.Repositories;

public class SpiritAnimalRepository
{
    private static readonly SpiritAnimalRepository Instance = new SpiritAnimalRepository();
    private List<SpiritAnimal> _spiritAnimals;

    public SpiritAnimalRepository()
    {
        _spiritAnimals = new List<SpiritAnimal>();
    }

    public static SpiritAnimalRepository GetInstance()
    {
        return Instance;
    }

    public List<SpiritAnimal> GetSpiritAnimals()
    {
        return _spiritAnimals;
    }

    public SpiritAnimal? GetSpiritAnimal(long id)
    {
        return _spiritAnimals.Find(spiritAnimal => spiritAnimal.Id == id);
    }

    public SpiritAnimal PutSpiritAnimal(SpiritAnimal existingAnimal, SpiritAnimal animal)
    {
        var animalIndex = _spiritAnimals.IndexOf(existingAnimal);
        _spiritAnimals[animalIndex] = animal;
        return _spiritAnimals[animalIndex];
    }

    public void PostSpiritAnimal(SpiritAnimal animal)
    {
        _spiritAnimals.Add(animal);
    }

    public void DeleteSpiritAnimal(SpiritAnimal animal)
    {
        _spiritAnimals.Remove(animal);
    }

    public void DeleteAllSpiritAnimals()
    {
        _spiritAnimals.Clear();
    }
}