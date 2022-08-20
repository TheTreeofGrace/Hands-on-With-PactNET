using System.Net;
using System.Net.Http.Headers;
using SpiritAnimalBackend.Models;

namespace PlaygroundCLI
{
    public class Client
    {
        readonly HttpClient _client;
        public Client(Uri baseUri)
        {
            // We are not handling SSL certs!
            var handler = new HttpClientHandler() 
            { 
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _client = new HttpClient(handler);
            _client.BaseAddress = baseUri;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetSpiritAnimal(long number)
        {
            return await _client.GetAsync($"api/SpiritAnimal/{number}");
        }

        public async Task<SpiritAnimal[]> GetSpiritAnimals()
        {
            SpiritAnimal[] spiritAnimal = new SpiritAnimal[]{};
            HttpResponseMessage httpResponse = await _client.GetAsync($"api/SpiritAnimal");
            if (httpResponse.IsSuccessStatusCode)
            {
                spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal[]>();
            }
            return spiritAnimal;
        }

        public async Task<Uri?> CreateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync(
                $"api/SpiritAnimal", spiritAnimal);
            return httpResponse.Headers.Location;
        }

        public async Task<SpiritAnimal> UpdateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            HttpResponseMessage httpResponse = await _client.PutAsJsonAsync(
                $"api/SpiritAnimal/{spiritAnimal.Id}",spiritAnimal);
            spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal>();
            return spiritAnimal;
        }

        public async Task<HttpStatusCode> DeleteSpiritAnimal(long number)
        {
            HttpResponseMessage httpResponse = await _client.DeleteAsync(
                $"api/SpiritAnimal/{number}");
            return httpResponse.StatusCode;
        }
    }
}

