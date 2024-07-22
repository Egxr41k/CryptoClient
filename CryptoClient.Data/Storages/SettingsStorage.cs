﻿using CryptoClient.Data.Serializers;
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

    public class SettingsStorage : Storage<SettingsDTO>
    {
        public SettingsStorage(
            ISerializer<SettingsDTO> serializer, 
            LoggingService loggingService, 
            string storageFilePath) : 
            base(serializer, loggingService, storageFilePath)
        {
            Content = ReadAsync().GetAwaiter().GetResult() ?? 
                new SettingsDTO();
        }
    }
}