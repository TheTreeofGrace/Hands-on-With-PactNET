using SpiritAnimalBackend.Models;

namespace SpiritAnimalBackend.Repositories;

public class SpiritAnimalRepository
{
    private static readonly SpiritAnimalRepository Instance = new ();
    private List<SpiritAnimal> _spiritAnimals;

    private SpiritAnimalRepository()
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
        try
        {
            _spiritAnimals.Add(animal);
        }
        catch (Exception e)
        {
            Console.WriteLine("Issue with adding a spirit animal: " + animal);
            Console.WriteLine(e.Message);
        }
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