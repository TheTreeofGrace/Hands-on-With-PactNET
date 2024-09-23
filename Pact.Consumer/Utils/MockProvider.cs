using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Pact.Consumer.Utils;

public class MockProvider
{
    private const int MockServerPort = 3001;
    protected readonly IPactBuilderV3 MockProviderServer;

    protected MockProvider(ITestOutputHelper output)
    {
        var config = new PactConfig
        {
            PactDir = $"{Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName}{Path.DirectorySeparatorChar}pact/pacts/",
            DefaultJsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            },
            Outputters = new List<IOutput>
            {
                new XUnitOutput(output)
            },
        };

        MockProviderServer = PactNet.Pact.V3("SpiritAnimalConsumer", "SpiritAnimalProvider", config).WithHttpInteractions(MockServerPort);
    }
}
