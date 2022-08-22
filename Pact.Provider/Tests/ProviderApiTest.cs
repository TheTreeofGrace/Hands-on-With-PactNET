using Pact.Provider.XUnitHelpers;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Xunit.Abstractions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Pact.Provider.Tests;

public class ProviderApiTest
{
    private readonly string _providerUri;
    private readonly string _brokerUri;
    private readonly string _pactPath;
    private readonly PactVerifierConfig _config;
    private readonly IWebHost _webHost;

        public ProviderApiTest(ITestOutputHelper output)
        {
            _providerUri = "http://localhost:3000";
            _brokerUri = "http://localhost:9292";
            _pactPath = Path.Join(@"../../../../", @"pact/pacts/SpiritAnimalConsumer-SpiritAnimalProvider.json");
            _config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(output)
                },
            };
            _webHost = WebHost.CreateDefaultBuilder().UseStartup<TestStartup>().UseUrls(_providerUri).Build();
            _webHost.Start();
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
            
        }
}