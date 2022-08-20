namespace SpiritAnimalBackend.Models
{
    public class SpiritAnimal
    {
        public SpiritAnimal(long id, string name, string colour)
        {
            Id = id;
            Name = name;
            Colour = colour;
        }
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Colour { get; set; }
            
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Colour: {Colour}";
        }
    }
    
}

