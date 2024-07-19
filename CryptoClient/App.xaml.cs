﻿using CryptoClient.Models;
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
        private readonly IApiService _apiService;
        private readonly HttpClient _httpClient;
        private readonly IStorageService _storageService;
        private readonly ISerializer _serializer;
        private readonly LoggingService _loggingService;

        public App()
        {
            _loggingService = new LoggingService();
            _settingsService = new SettingsService(_loggingService);
            _httpClient = new HttpClient();

            _apiService = _settingsService.Settings.UsedApi == "CoinCap" ?
                new CoinCapApiService(_httpClient, _loggingService) :
                new NbuApiService(_httpClient, _loggingService);

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
