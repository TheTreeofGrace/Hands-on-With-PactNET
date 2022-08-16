using System.Net;
using Pact.Consumer.Utils;
using PactNet.Matchers;
using PlaygroundCLI;
using Xunit.Abstractions;

namespace Pact.Consumer;

public class SpritConsumer : MockProvider
{
    private readonly Client _client;
    private const int Port = 1234;
    private readonly MinMaxTypeMatcher _spiritAnimals;
    private readonly object _spiritAnimal;
    
    public SpritConsumer(ITestOutputHelper output) : base(output)
    {
        _spiritAnimal = new {
            Id = new TypeMatcher(10),
            Colour = new TypeMatcher("CREDIT_CARD"),
            Name = new TypeMatcher("GEM Visa"),
        };
        _spiritAnimals = new MinMaxTypeMatcher( _spiritAnimal, 1, 2);
        _client = new Client(new Uri($"http://localhost:{Port}"));
    }
    
    [Fact]
    public async void GetAllSpiritAnimals()
    {
        // Arange
        MockProviderServer.UponReceiving("A request for all spirit animals")
            .Given("products exist")
            .WithRequest(HttpMethod.Get, "/SpiritAnimal")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithJsonBody(_spiritAnimals);

        // Act / Assert
        await MockProviderServer.VerifyAsync(async ctx =>
        {
            var response = await _client.GetSpiritAnimals();
            Assert.True(response.Length > 0);
        });
    }

    [Fact]
    public async void GetSpiritAnimal()
    {
        // Arange
        MockProviderServer.UponReceiving("A request for a spirit animals")
            .Given("products exist")
            .WithRequest(HttpMethod.Get, "/SpiritAnimal/10")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithJsonBody(_spiritAnimal);

        // Act / Assert
        await MockProviderServer.VerifyAsync(async ctx =>
        {
            var response = await _client.GetSpiritAnimal(10);
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        });
    }
}