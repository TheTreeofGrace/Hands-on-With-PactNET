using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Pact.Consumer.Utils;

public class MockProvider
{
    private const int MockServerPort = 1234;
    protected readonly IPactBuilderV3 MockProviderServer;

    protected MockProvider(ITestOutputHelper output)
    {
        var config = new PactConfig
        {
            PactDir = Path.Join(@"../../../../../", "pact/pacts"),
            DefaultJsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            },
            Outputters = new List<IOutput>
            {
                new XUnitOutput(output)
            },
        };

        MockProviderServer = PactNet.Pact.V3("SpiritAnimalConsumer", "SpiritAnimalProvider", config).UsingNativeBackend(MockServerPort);
    }
}
