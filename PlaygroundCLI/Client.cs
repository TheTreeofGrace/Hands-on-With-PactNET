using System.Net;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
        
        // TODO: API Helper File for GET, POST, PUT, DELETE

        public async Task<HttpResponseMessage> GetSpiritAnimal(long number)
        {
            return await _client.GetAsync($"SpiritAnimal/{number}");
        }

        public async Task<HttpResponseMessage> GetSpiritAnimals()
        {
            return await _client.GetAsync($"SpiritAnimal");
        }

        public async Task<HttpResponseMessage> PostSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            return await _client.PostAsJsonAsync(
                $"SpiritAnimal", spiritAnimal);
        }

        public async Task<HttpResponseMessage> PutSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            return await _client.PutAsJsonAsync(
                $"SpiritAnimal/{spiritAnimal.Id}", spiritAnimal);
        }

        public async Task<HttpResponseMessage> DeleteSpiritAnimal(long number)
        {
            return await _client.DeleteAsync(
                $"SpiritAnimal/{number}");
        }
        
        public async Task<SpiritAnimal> SpiritAnimal(long number)
        {
            var response = await GetSpiritAnimal(number);
            return await response.Content.ReadAsAsync<SpiritAnimal>();
        }

        public async Task<List<SpiritAnimal>> AllSpiritAnimals()
        {
            var response = await GetSpiritAnimals();
            var spiritAnimals = new List<SpiritAnimal>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    spiritAnimals = await response.Content.ReadAsAsync<List<SpiritAnimal>>();
                }
                catch (Exception e)
                {
                    Console.WriteLine("No spirit animals were returned");
                }
            }
            
            return spiritAnimals;
        }

        public async Task<Uri?> CreateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            var response = await PostSpiritAnimal(spiritAnimal);
            return response.Headers.Location;
        }

        public async Task<SpiritAnimal> UpdateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            var response = await PutSpiritAnimal(spiritAnimal);
            return await response.Content.ReadAsAsync<SpiritAnimal>();
        }

        public async Task<HttpStatusCode> RemoveSpiritAnimal(long number)
        {
            var response = await DeleteSpiritAnimal(number);
            return response.StatusCode;
        }
    }
}

