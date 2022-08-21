using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pact.Provider.Utils;
using SpiritAnimalBackend.Models;
using SpiritAnimalBackend.Repositories;

namespace Pact.Provider.Middleware;

public class ProviderStateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SpiritAnimalRepository _repository;
        private readonly IDictionary<string, Action> _providerStates;
        private readonly SpiritAnimal _unicorn = SpiritAnimalsMock.Unicorn;
        private readonly SpiritAnimal _dog = SpiritAnimalsMock.Dog;
        private readonly SpiritAnimal _panda = SpiritAnimalsMock.Panda;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _repository = SpiritAnimalRepository.GetInstance();
            _providerStates = new Dictionary<string, Action>
            {
                { "spirit animals exist", SpiritAnimalsExists},
                { "a spirit animal exists", SpiritAnimalExists }
            };
        }

        private void CleanUp()
        {
            _repository.DeleteAllSpiritAnimals();
        }

        private void SpiritAnimalsExists()
        {
            CleanUp();
            _repository.PostSpiritAnimal(_unicorn);
            // _repository.PostSpiritAnimal(_dog);
        }

        private void SpiritAnimalExists()
        {
            CleanUp();
            _repository.PostSpiritAnimal(_panda);
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/provider-states"))
            {
                await HandleProviderStatesRequest(context);
                await context.Response.WriteAsync(string.Empty);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleProviderStatesRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                context.Request.Body != null)
            {
                string jsonRequestBody = String.Empty;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = await reader.ReadToEndAsync();
                }

                var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (providerState != null && !String.IsNullOrEmpty(providerState.State))
                {
                    _providerStates[providerState.State].Invoke();
                }
            }
        }
    }