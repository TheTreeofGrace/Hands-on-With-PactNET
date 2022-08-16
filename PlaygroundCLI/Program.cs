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
            Client client = new();

            var spiritAnimal = new SpiritAnimal
            {
                Id = 0,
                Name = "Panda",
                Colour = "White"
            };
            
            var putSpiritAnimal = new SpiritAnimal
            {
                Id = 0,
                Name = "Panda",
                Colour = "Colourful"
            };

            var uri = await client.CreateSpiritAnimal(spiritAnimal);
            Console.WriteLine("API URL: " + uri);
                
            var getResult = await client.GetSpiritAnimal(0);
            Console.WriteLine("Spirit Animal at 1 is: " + getResult);
            
            var arr = await client.GetSpiritAnimals();
            Console.Write("All spirit animals: ");
            foreach (var animal in arr)
            {
                Console.Write(animal + ", ");
            }
            Console.WriteLine();

            var updated = await client.UpdateSpiritAnimal(putSpiritAnimal);
            Console.WriteLine("Updated Spirit Animal: " + updated);

            var arr2 = await client.GetSpiritAnimals();
            Console.Write("All spirit animals: ");
            foreach (var animal in arr2)
            {
                Console.Write(animal + ", ");
            }
            Console.WriteLine();

            var delete = await client.DeleteSpiritAnimal(spiritAnimal.Id);
            Console.WriteLine("Deleted Response: " + delete);
            
            arr = await client.GetSpiritAnimals();
            Console.WriteLine("All spirit animal are now: " + arr);
        }
    }
}