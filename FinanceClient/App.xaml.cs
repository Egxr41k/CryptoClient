using System.Net.Http;
using System.Windows;
using FinanceClient.Stores;
using FinanceClient.ViewModels;
using FinanceClient.Settings;
using FinanceClient.Data.Services;
using FinanceClient.Data.Storages;
using FinanceClient.Data.Serializers;
using FinanceClient.Logging;
using FinanceClient.Data.Models;

namespace FinanceClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SelectedModelStore _selectedModelStore;
        private readonly FinanceClientStore _cryptoClientStore;
        private readonly FinanceClientViewModel _cryptoClientViewModel;
        private readonly SettingsStorage _settingsService;
        private readonly IApiClient _apiService;
        private readonly IFetchService _fetchService;
        private readonly HttpClient _httpClient;
        private readonly CurrencyStorage _storageService;
        private readonly ISerializer<CurrencyModel[]> _serializer;
        private readonly ISerializer<SettingsDTO> _settingsSerializer;
        private readonly LoggingService _loggingService;

        public App()
        {
            _loggingService = new LoggingService();
            _settingsSerializer = new JsonSerializer<SettingsDTO>();
            
            _settingsService = new SettingsStorage(
                _settingsSerializer,
                _loggingService,
                "settings.json");

            _httpClient = new HttpClient();

            _fetchService = new JsonService(
                _httpClient,
                _loggingService);

            _apiService = _settingsService.Content.UsedApi == "CoinCap" ?
                new CoinCapClient(_fetchService) :
                new NbuClient(_fetchService);

            string storageName;

            switch (_settingsService.Content.FormatOfSaving)
            {
                case "JSON":
                    _serializer = new JsonSerializer<CurrencyModel[]>();
                    storageName = "storage.json";
                    break;
                case "XML":
                    _serializer = new XmlSerializer<CurrencyModel[]>();
                    storageName = "storage.xml";
                    break;
                case "CSV":
                    _serializer = new CsvSerializer();
                    storageName = "storage.csv";
                    break;
                default:
                    _serializer = new JsonSerializer<CurrencyModel[]>();
                    storageName = "storage.json";
                    break;
            }

            _storageService = new CurrencyStorage(
                _serializer, 
                _apiService,
                _loggingService,
                storageName);

            _cryptoClientStore = new FinanceClientStore();
            _selectedModelStore = new SelectedModelStore(
                _cryptoClientStore);

            _cryptoClientStore.CurrencyUpdated +=
                _selectedModelStore._cryptoClientStore_CurrencyUpdated;

            _cryptoClientViewModel = new FinanceClientViewModel(
                _cryptoClientStore, 
                _selectedModelStore,
                _storageService,
                _settingsService,
                _loggingService);
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
