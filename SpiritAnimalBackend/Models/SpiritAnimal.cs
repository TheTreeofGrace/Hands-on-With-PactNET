namespace SpiritAnimalBackend.Models
{
    public class SpiritAnimal
    {
        public SpiritAnimal(int id, string name, string colour)
        {
            Id = id;
            Name = name;
            Colour = colour;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Colour { get; set; }
            
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Colour: {Colour}";
        }
    }
    
}

