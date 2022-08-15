namespace PlaygroundAPI.Models
{
    public class SpiritAnimal
    {
            public long Id { get; set; }
            public string? Name { get; set; }
            public string? Colour { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Colour: {Colour}";
        }
    }
    
}

