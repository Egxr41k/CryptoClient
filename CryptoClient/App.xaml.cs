using System.Net.Http;
using System.Windows;
using CryptoClient.Stores;
using CryptoClient.ViewModels;
using CryptoClient.Settings;
using CryptoClient.Data.Services;
using CryptoClient.Data.Storages;
using CryptoClient.Data.Serializers;
using CryptoClient.Logging;

namespace CryptoClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedModelStore _selectedModelStore;
        private readonly CryptoClientStore _cryptoClientStore;
        private readonly CryptoClientViewModel _cryptoClientViewModel;
        private readonly SettingsService _settingsService;
        private readonly IApiClient _apiService;
        private readonly IFetchService _fetchService;
        private readonly HttpClient _httpClient;
        private readonly IStorageService _storageService;
        private readonly ISerializer _serializer;
        private readonly LoggingService _loggingService;

        public App()
        {
            _loggingService = new LoggingService();
            _settingsService = new SettingsService(_loggingService);
            _httpClient = new HttpClient();

            _fetchService = new JsonService(
                _httpClient,
                _loggingService);

            _apiService = _settingsService.Settings.UsedApi == "CoinCap" ?
                new CoinCapClient(_fetchService) :
                new NbuClient(_fetchService);

            string storageName;

            switch (_settingsService.Settings.FormatOfSaving)
            {
                case "JSON":
                    _serializer = new JsonSerializer();
                    storageName = "storage.json";
                    break;
                case "XML":
                    _serializer = new XmlSerializer();
                    storageName = "storage.xml";
                    break;
                case "CSV":
                    _serializer = new CsvSerializer();
                    storageName = "storage.csv";
                    break;
                default:
                    _serializer = new JsonSerializer();
                    storageName = "storage.json";
                    break;
            }

            _storageService = new StorageService(
                _serializer, 
                _apiService,
                _loggingService,
                storageName);

            _cryptoClientStore = new CryptoClientStore();
            _selectedModelStore = new SelectedModelStore(
                _cryptoClientStore);

            _cryptoClientStore.CurrencyUpdated +=
                _selectedModelStore._cryptoClientStore_CurrencyUpdated;

            _cryptoClientViewModel = new CryptoClientViewModel(
                _cryptoClientStore, 
                _selectedModelStore,
                _storageService,
                _settingsService);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_cryptoClientViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
