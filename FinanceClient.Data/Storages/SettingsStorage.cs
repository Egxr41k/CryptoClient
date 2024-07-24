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
        public readonly List<string> UsedApiList = new() 
            { "CoinCap", "NBU_Exchacnge" };

        public readonly List<int> AvailableCurrencyCountOptions = new() 
            { 5, 10 };

        public readonly List<int> FetchingIntervalMinOptions = new() 
            { 1, 3, 5, 10 };

        public readonly List<string> FormatOfSavingOptions = new() 
            { "JSON", "CSV", "XML" };

        public readonly Dictionary<string, SettingsDTO> CustomControlCodes = new()
        {
            { "DEFAULT", new SettingsDTO() },
            { "FAST_FETCH", new SettingsDTO 
                { 
                    UsedApi = "CoinCap", 
                    AvailableCurrencyCount = 5, 
                    FetchingIntervalMin = 1, 
                    FormatOfSaving = "JSON" 
                } 
            },
            { "CSV_STORAGE", new SettingsDTO 
                { 
                    UsedApi = "NBU_Exchacnge", 
                    AvailableCurrencyCount = 10, 
                    FetchingIntervalMin = 5, 
                    FormatOfSaving = "CSV" 
                } 
            },
            { "XML_STORAGE", new SettingsDTO 
                { 
                    UsedApi = "NBU_Exchacnge", 
                    AvailableCurrencyCount = 10, 
                    FetchingIntervalMin = 3, 
                    FormatOfSaving = "XML" 
                } 
            },
            { "EXTENDED_FETCH", new SettingsDTO 
                { 
                    UsedApi = "CoinCap", 
                    AvailableCurrencyCount = 10, 
                    FetchingIntervalMin = 10, 
                    FormatOfSaving = "JSON" 
                } 
            }
        };

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

        public void SetCustomControlCode(string code)
        {
            if (!CustomControlCodes.ContainsKey(code)) return;
            Content = CustomControlCodes[code];
            ContentChanged.Invoke();
        }
    }
}