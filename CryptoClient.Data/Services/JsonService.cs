using CryptoClient.Data.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Services
{
    public class JsonService : IFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly IStorageService _storageService;

        public JsonService(HttpClient httpClient, IStorageService storageService)
        {
            _httpClient = httpClient;
            _storageService = storageService;
        }

        public async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>(url);

                //storageService.Save
                return response ?? throw new Exception($"No data received from {url}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching data from {url}: {ex.Message}", ex);
            }
        }
    }
}
