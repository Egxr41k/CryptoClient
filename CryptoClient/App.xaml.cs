using CryptoClient.Models;
using CryptoClient.Stores;
using CryptoClient.ViewModels;
using CryptoClient.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CryptoClient.Data.Services;
using CryptoClient.Data.Storages;
using System.Windows.Threading;
using CryptoClient.Data.Serializers;

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
        private readonly IJsonService _jsonService;
        private readonly HttpClient _httpClient;
        private readonly IStorageService _storageService;
        private readonly ISerializer _serializer;

        public App()
        {
            _settingsService = new SettingsService();
            _httpClient = new HttpClient();

            _jsonService = _settingsService.Settings.UsedApi == "CoinCap" ?
                new CoinCapJsonService(_httpClient) :
                new NbuJsonService(_httpClient);

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
                _jsonService, 
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
