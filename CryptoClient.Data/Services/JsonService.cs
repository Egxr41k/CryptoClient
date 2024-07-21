using System.Net.Http.Json;
using System.Text.Json;
using CryptoClient.Logging;

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
                    _loggingService.WriteToLog(message);
                    throw new Exception(message);
                }
                _loggingService.WriteToLog("Data fetched successfully");
                return response;
            }
            catch (JsonException ex)
            {
                var response = await _httpClient.GetFromJsonAsync<T[]>(url);
                return response.First() ?? throw new Exception($"No data received from {url}");
            }
            catch (HttpRequestException ex)
            {
                var message = $"Error fetching data from {url}: {ex.Message}";
                _loggingService.WriteToLog(message);
                throw new Exception(message, ex);
            }
        }
    }
}
