﻿using CryptoClient.Stores;
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
        //private readonly SelectedModelStore _selectedModelStore;
        //private readonly CryptoClientStore _cryptoClientStore;
        private readonly CryptoClientViewModel _cryptoClientViewModel;
        HomeViewModel HomeVM;
        ConvertViewModel ConvertVM;
        public App()
        {
            HomeVM = new();
            ConvertVM = new();
            CryptoClientStore.CurrencyUpdated += 
                SelectedModelStore._cryptoClientStore_CurrencyUpdated;
            //_cryptoClientStore = new CryptoClientStore();
            //_selectedModelStore = new SelectedModelStore(_cryptoClientStore);

            _cryptoClientViewModel = new CryptoClientViewModel();
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
