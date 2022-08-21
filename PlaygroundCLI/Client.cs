using System.Net;
using SpiritAnimalBackend.Models;

namespace PlaygroundCLI
{
    public class Client
    {
        readonly HttpClient _client;
        public Client(Uri baseUri)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        public async Task<HttpResponseMessage> GetSpiritAnimal(long number)
        {
            return await _client.GetAsync($"SpiritAnimal/{number}");
        }

        public async Task<HttpResponseMessage> GetSpiritAnimals()
        {
            // var spiritAnimal = new List<SpiritAnimal>(){};
            var httpResponse = await _client.GetAsync($"SpiritAnimal");
            return httpResponse;
        }

        public async Task<Uri?> CreateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(
                $"SpiritAnimal", spiritAnimal);
            return httpResponse.Headers.Location;
        }

        public async Task<SpiritAnimal> UpdateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            HttpResponseMessage httpResponse = await _client.PutAsJsonAsync(
                $"SpiritAnimal/{spiritAnimal.Id}",spiritAnimal);
            spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal>();
            return spiritAnimal;
        }

        public async Task<HttpStatusCode> DeleteSpiritAnimal(long number)
        {
            HttpResponseMessage httpResponse = await _client.DeleteAsync(
                $"SpiritAnimal/{number}");
            return httpResponse.StatusCode;
        }
    }
}

