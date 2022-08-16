using SpiritAnimalBackend.Models;

namespace PlaygroundCLI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting");

            Console.WriteLine("Running");
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            
            Client client = new();
            Console.WriteLine("loop");
            
                SpiritAnimal spiritAnimal = new SpiritAnimal
                {
                    Name = "Panda",
                    Colour = "White"

                };
                SpiritAnimal upspiritAnimal = new SpiritAnimal
                {
                    Id = 1,
                    Name = "Panda",
                    Colour = "Colourful"

                };

                Uri uri = await client.CreateSpiritAnimal(spiritAnimal);
                Console.WriteLine(uri);


                SpiritAnimal SA = await client.GetSpiritAnimal(1);
                Console.WriteLine(SA);
                SpiritAnimal[] arr = await client.GetSpiritAnimals();
                Console.WriteLine("All");
                foreach (var animal in arr)
                {
                    Console.WriteLine(animal);
                }

                SpiritAnimal updated = await client.UpdateSpiritAnimal(upspiritAnimal);
                Console.WriteLine(updated);

            arr = await client.GetSpiritAnimals();
            Console.WriteLine("All");
            foreach (var animal in arr)
            {
                Console.WriteLine(animal);
            }


        }

    }
}