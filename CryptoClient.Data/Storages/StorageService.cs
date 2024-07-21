using CryptoClient.Data.Serializers;
using CryptoClient.Data.Services;
using CryptoClient.Logging;
using CryptoClient.Data.Models;

namespace CryptoClient.Data.Storages
{
    public class StorageService
    {
        private readonly ISerializer _serializer;
        private readonly IApiClient _apiService;
        private readonly LoggingService _loggingService;
        private readonly string _storageFilePath;

        public Action ContentChanged;
        public StorageService(
            ISerializer serializer,
            IApiClient apiService,
            LoggingService loggingService,
            string storageFilePath)
        {
            _serializer = serializer;
            _apiService = apiService;
            _loggingService = loggingService;

            _storageFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                storageFilePath);
        }

        public void ClearStorage()
        {
            File.WriteAllText(_storageFilePath, "");
            ContentChanged.Invoke();
        }

        public string GetUnserializedData()
        {
            return File.ReadAllText(_storageFilePath);
        }

        public async Task<CurrencyModel[]> ReadAsync()
        {
            if (!File.Exists(_storageFilePath))
            {
                return await UpdateAsync();
            }

            try
            {
                return await _serializer.DeserializeAsync(_storageFilePath);
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLog($"An error occurred while reading data: {ex.Message}");
                return await UpdateAsync();
            }
        }

        public async Task SaveAsync(CurrencyModel[] data)
        {
            try
            {
                await _serializer.SerializeAsync(data, _storageFilePath);
                ContentChanged.Invoke();
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLog($"An error occurred while saving data: {ex.Message}");
            }
        }

        public async Task<CurrencyModel[]> UpdateAsync()
        {
            try
            {
                // Replace this with actual logic to fetch new data
                CurrencyModel[] newData = await _apiService.GetFullCurrenciesInfoAsync();
                await SaveAsync(newData);
                return newData;
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLog($"An error occurred while updating data: {ex.Message}");
                throw;
            }
        }
    }
}
