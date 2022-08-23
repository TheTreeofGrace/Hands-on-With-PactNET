using SpiritAnimalBackend.Models;

namespace PlaygroundCLI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting DevOps Playground Spirit Animal API");
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            Client client = new(new Uri($"http://localhost:3000"));

            var spiritAnimal = new SpiritAnimal(0, "Panda", "Red");
            
            var putSpiritAnimal = new SpiritAnimal(0, "Panda", "Black and White");

            var uri = await client.CreateSpiritAnimal(spiritAnimal);
            Console.WriteLine("API URL: " + uri);
                
            var getResult = await client.SpiritAnimal(0);
            Console.WriteLine("Spirit Animal at 1 is: " + getResult);
            
            var spiritAnimals1 = await client.AllSpiritAnimals();
            Console.Write("All spirit animals: ");
            foreach (var animal in spiritAnimals1)
            {
                Console.Write(animal + ", ");
            }
            Console.WriteLine();

            var updated = await client.UpdateSpiritAnimal(putSpiritAnimal);
            Console.WriteLine("Updated Spirit Animal: " + updated);

            var spiritAnimals2 = await client.AllSpiritAnimals();
            Console.Write("All spirit animals: ");
            foreach (var animal in spiritAnimals2)
            {
                Console.Write(animal + ", ");
            }
            Console.WriteLine();

            var delete = await client.RemoveSpiritAnimal(spiritAnimal.Id);
            Console.WriteLine("Deleted Response: " + delete);
            
            var spiritAnimals3 = await client.AllSpiritAnimals();

            if (spiritAnimals3 == null)
            {
                Console.WriteLine("All spirit animals are now removed!");
            }
            else
            {
                Console.WriteLine("All spirit animal are now: " + spiritAnimals3);
            }
            
        }
    }
}