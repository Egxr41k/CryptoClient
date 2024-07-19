using CryptoClient.Data.Storages;
using CryptoClient.Logging;
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
        private readonly LoggingService _loggingService;

        public JsonService(HttpClient httpClient, LoggingService loggingService)
        {
            _httpClient = httpClient;
            _loggingService = loggingService;
        }

        public async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>(url);
                if (response == null)
                {
                    var message = $"No data received from {url}";
                    _loggingService.WriteLine(message);
                    throw new Exception(message);
                }
                _loggingService.WriteLine("Data fetched successfully");
                return response;
            }
            catch (HttpRequestException ex)
            {
                var message = $"Error fetching data from {url}: {ex.Message}";
                _loggingService.WriteLine(message);
                throw new Exception(message, ex);
            }
        }
    }
}
