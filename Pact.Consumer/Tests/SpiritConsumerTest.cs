using System.Net;
using Pact.Consumer.Utils;
using PactNet.Matchers;
using PlaygroundCLI;
using Xunit.Abstractions;

namespace Pact.Consumer.Tests;

public class SpiritConsumerTest : MockProvider
{
    private readonly Client _client;
    private const int Port = 3001;
    private readonly MinMaxTypeMatcher _spiritAnimals;
    private readonly object _spiritAnimal;
    
    public SpiritConsumerTest(ITestOutputHelper output) : base(output)
    {
        _spiritAnimal = new {
            Id = new TypeMatcher(10),
            Colour = new TypeMatcher("Red"),
            Name = new TypeMatcher("Panda"),
        };
        _spiritAnimals = new MinMaxTypeMatcher( _spiritAnimal, 1, 2);
        _client = new Client(new Uri($"http://localhost:{Port}"));
    }
    
    [Fact]
    public async void GetAllSpiritAnimals()
    {
        
    }

    [Fact]
    public async void GetSpiritAnimal()
    {
        
    }
}