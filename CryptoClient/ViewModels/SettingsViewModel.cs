using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Data.Models;
using CryptoClient.Data.Storages;
using CryptoClient.Logging;
using CryptoClient.Settings;

namespace CryptoClient.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private readonly SettingsStorage _settingsService;
        private readonly LoggingService _loggingService;
        private readonly Storage<CurrencyModel[]> _storageService;
        public ICommand ApplyChangesCommand { get; set; }
        public ICommand ClearStorageCommand { get; set; }
        public ICommand ClearLogsCommand { get; set; }
        public List<string> UsedApis { get; }

        private string selectedUsedApi;
        public string SelectedUsedApi
        {
            get => selectedUsedApi;
            set
            {
                SetProperty(ref selectedUsedApi, value);
                _settingsService.Content.UsedApi = value;
            }
        }

        public List<int> AvailableCurrencyCounts { get; }

        private int selectedAvailableCurrencyCount;
        public int SelectedAvailableCurrencyCount
        {
            get => selectedAvailableCurrencyCount;
            set
            {
                SetProperty(ref selectedAvailableCurrencyCount, value);
                _settingsService.Content.AvailableCurrencyCount = value;
            }
        }

        public List<int> FetchingIntervalsMin { get; }

        private int selectedFetchingIntervalMin;
        public int SelectedFetchingIntervalMin
        {
            get => selectedFetchingIntervalMin;
            set 
            {
                SetProperty(ref selectedAvailableCurrencyCount, value);
                _settingsService.Content.FetchingIntervalMin = value;
            }
        }

        public List<string> FormatsOfSaving { get; }

        private string selectedFormatOfSaving;
        public string SelectedFormatOfSaving
        {
            get => selectedFormatOfSaving;
            set
            {
                SetProperty(ref selectedFormatOfSaving, value);
                _settingsService.Content.FormatOfSaving = value;
            }
        }

        public string log;
        public string Log
        {
            get => log;
            set => SetProperty(ref log, value);
        }

        private string unserializedData;
        public string UnserializedData
        {
            get => unserializedData;
            set => SetProperty(ref unserializedData, value);
        }

        private ScrollViewer logScrollViewer;
        public ScrollViewer LogScrollViewer
        {
            get => logScrollViewer;
            set => SetProperty(ref logScrollViewer, value);
        }


        public SettingsViewModel(
            SettingsStorage settingsService,
            CurrencyStorage storageService,
            LoggingService loggingService)
        {
            _settingsService = settingsService;
            _storageService = storageService;
            _loggingService = loggingService;

            storageService.ContentChanged +=
                storageService_ContentChanged;

            loggingService.ContentChanged +=
            loggingService_ContentChanged;

            UsedApis = new List<string>() { "CoinCap", "NBU_Exchacnge"};

            AvailableCurrencyCounts = new List<int>() { 5, 10 };

            FetchingIntervalsMin = new List<int>() { 1, 3, 5, 10 };

            FormatsOfSaving = new List<string>() { "JSON", "CSV", "XML" };

            LogScrollViewer = new ScrollViewer();

            selectedUsedApi = _settingsService.Content.UsedApi;
            selectedAvailableCurrencyCount = _settingsService.Content.AvailableCurrencyCount;
            selectedFetchingIntervalMin = _settingsService.Content.FetchingIntervalMin;
            selectedFormatOfSaving = _settingsService.Content.FormatOfSaving;

            ClearStorageCommand = new RelayCommand(() => 
                _storageService.ClearStorage());

            ClearLogsCommand = new RelayCommand(() =>
                _loggingService.ClearLog());

            ApplyChangesCommand = new RelayCommand(async() =>
            {
                await _settingsService.SaveAsync(_settingsService.Content);
                RestartApplication();
            });
        }

        private void storageService_ContentChanged()
        {
            UnserializedData = _storageService.GetUnserializedData();
        }

        private void loggingService_ContentChanged()
        {
            Log = _loggingService.GetLog();
            LogScrollViewer.ScrollToBottom();
        }

        private async void RestartApplication()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = currentProcess.MainModule.FileName,
                UseShellExecute = false
            };

            System.Diagnostics.Process.Start(startInfo);

            // Small delay to allow the new instance to start
            await Task.Delay(1000);

            Application.Current.Shutdown();
        }
    }
}
