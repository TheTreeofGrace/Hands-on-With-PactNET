using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PlaygroundAPI.Models;
namespace PlaygroundCLI
{
    public class Client
    {
        HttpClient client;
        public Client()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:3000/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<SpiritAnimal> GetSpiritAnimal(int number)
        {
            SpiritAnimal spiritAnimal = new();
            HttpResponseMessage httpResponse = await client.GetAsync($"SpiritAnimal/{number}");
            if(httpResponse.IsSuccessStatusCode)
            {
                spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal>();
            }
            return spiritAnimal;
        }

        public async Task<SpiritAnimal[]> GetSpiritAnimals()
        {
            SpiritAnimal[] spiritAnimal = new SpiritAnimal[] { };
            HttpResponseMessage httpResponse = await client.GetAsync("SpiritAnimal");
            if (httpResponse.IsSuccessStatusCode)
            {
                spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal[]>();
            }
            return spiritAnimal;
        }

        public async Task<Uri?> CreateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync(
                "SpiritAnimal", spiritAnimal);
            return httpResponse.Headers.Location;
        }

        public async Task<SpiritAnimal> UpdateSpiritAnimal(SpiritAnimal spiritAnimal)
        {
            Console.WriteLine(spiritAnimal);
            HttpResponseMessage httpResponse = await client.PutAsJsonAsync(
                $"SpiritAnimal/{spiritAnimal.Id}",spiritAnimal);
            Console.WriteLine(httpResponse.StatusCode);
            spiritAnimal = await httpResponse.Content.ReadAsAsync<SpiritAnimal>();
            return spiritAnimal;
        }

        public async Task<HttpStatusCode> DeleteSpiritAnimal(int number)
        {
            HttpResponseMessage httpResponse = await client.DeleteAsync(
                $"SpiritAnimal/{number}");
            return httpResponse.StatusCode;
        }
    }
}

