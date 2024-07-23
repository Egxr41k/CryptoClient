using FinanceClient.Data.Contracts;
using FinanceClient.Data.Models;
using FinanceClient.Data.Serializers;
using FinanceClient.Data.Services;
using FinanceClient.Logging;
using FinanceClient.Settings;

namespace FinanceClient.Data.Storages
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
            var newData = await _apiService.GetFullCurrenciesInfoAsync();
            var oldData = await ReadAsync();

            if (newData == null || 
                newData == oldData || 
                newData.Length == 0) 
                return Array.Empty<CurrencyModel>();

            await SaveAsync(newData);
            return newData;
        }
    }
}
