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
    private readonly PactVerifierConfig _config;
    private readonly IWebHost _webHost;

        public ProviderApiTest(ITestOutputHelper output)
        {
            _providerUri = "http://localhost:4444";
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
            // Arrange
            var pactPath = Path.Join(@"../../../../", @"pact/pacts/SpiritAnimalConsumer-SpiritAnimalProvider.json");

            IPactVerifier pactVerifier = new PactVerifier(_config);

            // Act / Assert
            pactVerifier
                .ServiceProvider("SpiritAnimalProvider", new Uri(_providerUri))
                .WithFileSource(new FileInfo(pactPath))
                .WithProviderStateUrl(new Uri($"{_providerUri}/provider-states"))
                .Verify();
        }
}