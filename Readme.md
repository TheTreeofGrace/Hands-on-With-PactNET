### Set Up (Your own machine)
This playground uses the following technology so you will need to check they are installed:

- Web Browser
- Code IDE (we use VS Code)
- Docker
- Docker Compose
- .NET 6.0
- Terminal (We use Bash)
- Pact-Broker cli tool: `gem install --user-install pact_broker-client`
- dotnet cli tool: 
- git

# Getting Started


# Pact Consumer Tests

# Pact Provider Tests

# Pact Broker

## Starting the pact broker docker
```
cd ..
cd PactBroker
docker-compose up -d
```
-> Pact Broker UI url: http://localhost 

## Publishing the pacts to the broker

Publish pacts: `pact-broker publish pact/pacts/ --consumer-app-version 1 --broker-base-url http://localhost:9292`
## Verifying Provider Tests

Now that we have our pact files published to the broker we will need to change our Provider tests in order to use this. 

Open the Provider Tests file located at `Pact.Provider/Tests/ProviderApiTest`. Here we will need to update each test's pactVerifier code block with the following:

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

Notice here that we have change the `.WithFileSource` with `.WithPactBrokerSource`. This is where we can provide the url to the pact broker in order for it to use the pact broker API. Inside of this method we also handle specifying the provider version. This is important in order for the Pact Broker to be able to track and successfully verify the contracts.

Now we will run the Provider tests once again by navigating to the `Pact.Provider` folder and running the tests:

```
cd ..
cd Pact.Provider
dotnet test
```

Check the Pact Broker: `http://localhost` and notice we know have the Last Verified column has been updated and showing green to show success. :tada:

We can further check the verification by clicking through back to the pact file. In the top right you will notice a verified web badge. 

![](./Images/pact-verified-broker-badge.png)

# Bonus
So far we have only done two basic GET contract tests to the `SpiritAnimal` endpoint. Looking at the swagger document () we can see there are still `POST`, `PUT` and `DELETE` methods that could have tests written. 

Using what you have learned from implementing the `GET` tests try to write tests for the other methods of the API that are permitted. 

There is also negative test scenarios that could be tested too. For example when there are no spirit animals to be returned or the requested spirit animal does not exist. 

