`cd PactBroker`
`docker-compose up -d`
-> Pact Broker UI url: http://localhost:80 
`docker-compose down` -> to kill docker containers

Install the Pact-Broker cli tool: `gem install --user-install pact_broker-client`

Publish pacts: `pact-broker publish pact/pacts/ --consumer-app-version 1 --broker-base-url http://localhost:9292`

