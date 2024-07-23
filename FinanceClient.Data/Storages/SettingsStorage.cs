using FinanceClient.Data.Serializers;
using FinanceClient.Data.Storages;
using FinanceClient.Logging;

namespace FinanceClient.Settings
{
    public class SettingsDTO
    {
        public string UsedApi { get; set; }
        public int AvailableCurrencyCount { get; set; }
        public int FetchingIntervalMin { get; set; }
        public string FormatOfSaving { get; set; }

        public SettingsDTO()
        {
            UsedApi = "CoinCap";
            AvailableCurrencyCount = 10;
            FetchingIntervalMin = 5;
            FormatOfSaving = "JSON";
        }
    }

    public class SettingsStorage : Storage<SettingsDTO>
    {
        public SettingsStorage(
            ISerializer<SettingsDTO> serializer, 
            LoggingService loggingService, 
            string storageFilePath) : 
            base(serializer, loggingService, storageFilePath)
        {

            var settings = ReadAsync().GetAwaiter().GetResult();

            if (settings == null)
            {
                settings = new SettingsDTO();
                SaveAsync(settings).RunSynchronously();
            }
            Content = settings;
        }
    }
}