using System.Net.Http.Json;
using System.Text.Json;
using CryptoClient.Logging;

namespace CryptoClient.Data.Services
{
    public class JsonService : IFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggingService _loggingService;

        public JsonService(
            HttpClient httpClient,
            LoggingService loggingService)
        {
            _httpClient = httpClient;
            _loggingService = loggingService;
        }

        public async Task<T?> FetchDataAsync<T>(string url)
        {
            try
            {
                var response = await FetchAsync<T>(url);
                return response;
            }
            catch (JsonException ex)
            {
                var responce = await FetchAsync<T[]>(url) ?? Array.Empty<T>();
                return responce.First();
            }
            catch (HttpRequestException ex)
            {
                _loggingService.WriteToLog($"Error fetching data from {url}: {ex.Message}");
                //throw new Exception($"Error fetching data from {url}: {ex.Message}");
                return default;
            }
        }

        private async Task<T?> FetchAsync<T>(string url)
        {
            var response = await _httpClient.GetFromJsonAsync<T>(url);
            if (response == null)
            {
                _loggingService.WriteToLog($"No data received from {url}");
                //throw new Exception($"No data received from {url}");
                return default;
            }
            else
            {
                _loggingService.WriteToLog($"Data fetched successfully from {url}");
                return response;
            }
        }
    }
}
