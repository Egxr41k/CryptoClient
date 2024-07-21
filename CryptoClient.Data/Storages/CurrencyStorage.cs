using CryptoClient.Data.Models;
using CryptoClient.Data.Serializers;
using CryptoClient.Data.Services;
using CryptoClient.Logging;
using CryptoClient.Settings;

namespace CryptoClient.Data.Storages
{
    public class CurrencyStorage : Storage<CurrencyModel[]>
    {
        private readonly IApiClient _apiService;

        public CurrencyStorage(
            ISerializer<CurrencyModel[]> serializer,
            IApiClient apiService,
            LoggingService loggingService,
            string storageFilePath) : 
            base(serializer, loggingService, storageFilePath)
        {
            _apiService = apiService;
            Content = Array.Empty<CurrencyModel>();
        }

        public async Task<CurrencyModel[]> UpdateAsync()
        {
            try
            {
                var newData = await _apiService.GetFullCurrenciesInfoAsync();
                await SaveAsync(newData);
                return newData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
