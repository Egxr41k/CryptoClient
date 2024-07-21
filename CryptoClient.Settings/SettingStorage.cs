using CryptoClient.Data.Serializers;
using CryptoClient.Data.Storages;
using CryptoClient.Logging;

namespace CryptoClient.Settings
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

    public class SettingStorage : Storage<SettingsDTO>
    {
        public SettingStorage(
            ISerializer<SettingsDTO> serializer, 
            LoggingService loggingService, 
            string storageFilePath) : 
            base(serializer, loggingService, storageFilePath)
        {
        }
    }
}