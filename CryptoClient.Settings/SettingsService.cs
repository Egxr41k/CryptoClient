using System.Text.Json;
using CryptoClient.Logging;

namespace CryptoClient.Settings
{
    public class SettingsService
    {
        private readonly string _settingsFilePath;
        private readonly LoggingService _loggingService;
        public SettingsDTO Settings { get; private set; }

        public SettingsService(LoggingService loggingService, string settingsFilePath = "last-settings.json")
        {
            _loggingService = loggingService;

            _settingsFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                settingsFilePath);

            Settings = LoadSettings();
        }

        private SettingsDTO LoadSettings()
        {
            if (!File.Exists(_settingsFilePath))
            {
                return GetDefaultSettings();
            }

            try
            {
                string jsonString = File.ReadAllText(_settingsFilePath);
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var settings = JsonSerializer.Deserialize<SettingsDTO>(jsonString, jsonOptions);
                return settings ?? GetDefaultSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading settings: {ex.Message}");
                return GetDefaultSettings();
            }
        }

        private SettingsDTO GetDefaultSettings()
        {
            return new SettingsDTO
            {
                UsedApi = "CoinCap",
                AvailableCurrencyCount = 10,
                FetchingIntervalMin = 5,
                FormatOfSaving = "JSON"
            };
        }

        public void SaveSettings()
        {
            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(Settings, jsonOptions);
                File.WriteAllText(_settingsFilePath, jsonString);
            }
            catch (Exception ex)
            {
                _loggingService.WriteLine($"An error occurred while saving settings: {ex.Message}");
            }
        }
    }
}