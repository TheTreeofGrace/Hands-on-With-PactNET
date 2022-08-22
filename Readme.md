### Prerequisites (Your own machine)
This playground uses the following technology so you will need to check they are installed:

- Web Browser
- Code IDE (we use VS Code)
- Docker
- Docker-Compose
- C# .NET 6 from SDK: https://docs.microsoft.com/en-us/dotnet/core/install/
- Terminal (We use Bash)
- Ruby: https://www.ruby-lang.org/en/documentation/installation/
- Pact-Broker cli tool: `gem install --user-install pact_broker-client`
- git: to clone this repo!

***Network***
The following ports will be used for the application and testing:
- 80 -> pack broker
- 3000 -> backend api
- 9292 -> postgress db for pact broker
- 3001 -> frontend api
- 443 -> some port listed in docker-compose file but we are not going to directly use this one

Pact Broker Url: http://localhost
Backend API: http://localhost:3000/SpiritAnimal
Backend API Swagger: http://localhost:3000/swagger

Remember to change any urls to localhost if you are following on your own machine!

# Getting Started
The playground uses C# .NET 6 with version 4 of pact. Although no existing knowledge of C# .NET will be needed for this playground it could be benificial to have basic knowledge about some terms used for programming. Such as:
- Code block
- Function/Method
- Veriable
- Class

Before getting started, if using our infrastructure be sure to get your instance details. Open up this repo in your IDE to be able to look through the files.

### Good commands to know:
- `cd <dirName>` to move into a directory
- `cd ..` to move out of a directory
- `ls` to list directories and files of your current location
- `pwd` if you get lost where you are in the directories will give full path location
- `dotnet run` to run the backend and frontend APIs
- `dotnet test` to run the pact tests
- `docker-compose up -d` to start the docker containers
- `docker-compose down` to stop the containers

# Introduction
PactNet is a .NET implementation of Pact that allows you to define a pact between two ends of an API connection (or relationship). Pact refers to these as cunsumers and providers. The consumer is the consuming API, otherwise known as the "frontend" API. The provider is the provider of a service being consumer, otherwise known as the backend API.

Pact provides a DSL(Domain Specific Lanugage) for consumers to define the request they will make to a provider along with the response they expect back. This expectation is used to create the mock provider that is then played back to the real provider with the pact file that is produced. 

![](https://docs.pact.io/img/how-pact-works/summary.png)
Example diagram of the relationship architecture from docs.pact.io

# Run the APIs
To get started we will first run the provider API (SpiritAnimalBackend). Run the following commands to navigate into the correct directory from your teminal.

```
cd SpiritAnimalBackend
dotnet run
```
You should then get output in the terminal similar to the following:

![](./Images/backend-api-running.png)

Now open the swagger url in your browser: http://localhost:3000/swagger

![](./Images/swagger.png)

Here we will be able to do a bit of manual testing with the backend api such as POST a spirit animal, GET a spirit animal. We can also see there is ability for PUT and DELETE request handling too.

Let's post a spirit animal by clicking on `POST` and `Try it out`

![](./Images/post-try-it-out.png)

Fill in the parameters with what you like and click `Execute`. You should get the below showing 201 success and the Spirit Animal object you just created.

![](./Images/post-spirit-animal.png)

Further confirm your Spirit Animal has been created by running the `GET` request the same way. `Try it out` then `Execute`. Notice that there are no parameters for this method.

![](./Images/get-try-it-out.png)

Now lets run the frontend console application that contains the cunsumer side API. For this you will need to open a terminal in VS Code (located at the bottom of the IDE or on the top navigation menu).

```
cd PlaygroundCLI
dotnet run
```

Read through the below output. We can see that the Spirit Animal you created earlier has been outputted into the console along with some other spirit animals being created and one being deleted!

TODO: Fix CLI
# Pact Consumer Tests


# Pact Provider Tests

# Breaking the Contracts
# Pact Broker

TODO: a bit about the pact broker

## Starting the Pact Broker in docker
```
cd ..
cd PactBroker
docker-compose up -d
```

Check the pact broker is running: http://localhost you may notice that there is a default example pact file already there. Feel free to delete this pact file as we will not be using it.

TOOD: Insert image of pact Broker

## Publishing the Pacts
Now we have our pact broker up and running we will be able to publish the pact files created by the consumer tests. Run the following commands in your terminal:
```
cd ..
pact-broker publish pact/pacts/ --consumer-app-version 1 --broker-base-url http://localhost:9292
```

You should get something like the below output: 
TODO: Insert image

Now if we go to the Pact Broker we can see it has our pact file.
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
## Breaking the Contracts
Run the same changes we did earlier for breaking the contracts. Take a look at how the Pact Broker handles these test failures. 

## Want to write more tests?
So far we have only done two basic GET contract tests to the `SpiritAnimal` endpoint. Looking at the swagger document () we can see there are still `POST`, `PUT` and `DELETE` methods that could have tests written. 

Using what you have learned from implementing the `GET` tests try to write tests for the other methods of the API that are permitted. 

There is also negative test scenarios that could be tested too. For example when there are no spirit animals to be returned or the requested spirit animal does not exist. 

# References
## Pact
Pact Wiki: https://docs.pact.io/
Pact .NET Example Repo (using old version of pact): https://github.com/DiUS/pact-workshop-dotnet-core-v3/
## Pact Broker
Pact Broker Wiki: https://docs.pact.io/pact_broker
Pact Broker docker-compose file: https://github.com/pact-foundation/pact-broker-docker/blob/master/docker-compose.yml
Pact Broker repo: https://github.com/pact-foundation/pact-broker-docker/blob/master
