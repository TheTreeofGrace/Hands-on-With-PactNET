`cd PactBroker`
`docker-compose up -d`
-> Pact Broker UI url: http://localhost:80 
`docker-compose down` -> to kill docker containers

Install the Pact-Broker cli tool: `gem install --user-install pact_broker-client`

Publish pacts: `pact-broker publish pact/pacts/ --consumer-app-version 1 --broker-base-url http://localhost:9292`

# Run provider tests against the Pact Broker

Update the file `ProviderApiTest` in the `Pact.Provider` folder. 

For each test in the file update the pactVerifier code block:
```
pactVerifier
    .ServiceProvider("SpiritAnimalProvider", new Uri(_providerUri))
    .WithPactBrokerSource(new Uri(_brokerUri),options =>
    {
        options.ConsumerVersionSelectors(new ConsumerVersionSelector { Latest = true })
        .PublishResults(providerVersion:"1");
    })
    .WithProviderStateUrl(new Uri($"{_providerUri}/provider-states"))
    .Verify();
```

