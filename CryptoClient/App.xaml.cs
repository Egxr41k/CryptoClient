﻿using CryptoClient.Models;
using CryptoClient.Services;
using CryptoClient.Stores;
using CryptoClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

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
        private readonly IJsonService _jsonService;
        private readonly HttpClient _httpClient;

        public App()
        {
            _httpClient = new HttpClient();
            _jsonService = new NbuJsonService(_httpClient);
            _cryptoClientStore = new CryptoClientStore();
            _selectedModelStore = new SelectedModelStore(
                _cryptoClientStore);

            _cryptoClientStore.CurrencyUpdated +=
                _selectedModelStore._cryptoClientStore_CurrencyUpdated;

            _cryptoClientViewModel = new CryptoClientViewModel(
                _cryptoClientStore, 
                _selectedModelStore, 
                _jsonService);
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
