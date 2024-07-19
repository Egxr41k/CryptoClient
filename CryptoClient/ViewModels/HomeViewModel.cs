using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Settings;

namespace CryptoClient.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public string Text { get; set; } = "HomeView";

        private SettingsService _settingsService;

        public ICommand RestartCommand { get; set; }
        public List<string> UsedApis { get; }

        public string selectedUsedApi;
        public string SelectedUsedApi
        {
            get => selectedUsedApi;
            set
            {
                SetProperty(ref selectedUsedApi, value);
                _settingsService.Settings.UsedApi = value;
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
                _settingsService.Settings.AvailableCurrencyCount = value;
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
                _settingsService.Settings.FetchingIntervalMin = value;
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
                _settingsService.Settings.FormatOfSaving = value;
            }
        }

        public HomeViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;

            UsedApis = new List<string>() { "CoinCap", "NBU_Exchacnge"};

            AvailableCurrencyCounts = new List<int>() { 5, 10 };

            FetchingIntervalsMin = new List<int>() { 1, 3, 5, 10 };

            FormatsOfSaving = new List<string>() { "JSON", "CSV", "XML" };

            selectedUsedApi = _settingsService.Settings.UsedApi;
            selectedAvailableCurrencyCount = _settingsService.Settings.AvailableCurrencyCount;
            selectedFetchingIntervalMin = _settingsService.Settings.FetchingIntervalMin;
            selectedFormatOfSaving = _settingsService.Settings.FormatOfSaving;


            RestartCommand = new RelayCommand(() =>
            {
                _settingsService.SaveSettings();
                RestartApplication();
            });
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
