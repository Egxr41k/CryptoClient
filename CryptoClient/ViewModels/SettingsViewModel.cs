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
    public class SettingsViewModel : ObservableObject
    {
        private readonly SettingsStorage _settingsService;
        public ICommand ApplyChangesCommand { get; set; }
        public InfoViewModel InfoViewModel { get; set; }

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
        public List<string> UsedApiList { get; }

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
        public List<int> AvailableCurrencyCountList { get; }

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
        public List<int> FetchingIntervalMinList { get; }

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
        public List<string> FormatOfSavingList { get; }

        public SettingsViewModel(
            SettingsStorage settingsService,
            InfoViewModel infoViewModel)
        {
            _settingsService = settingsService;
            InfoViewModel = infoViewModel;

            UsedApiList = new List<string>() { "CoinCap", "NBU_Exchacnge" };

            AvailableCurrencyCountList = new List<int>() { 5, 10 };

            FetchingIntervalMinList = new List<int>() { 1, 3, 5, 10 };

            FormatOfSavingList = new List<string>() { "JSON", "CSV", "XML" };

            selectedUsedApi = _settingsService.Content.UsedApi;
            selectedAvailableCurrencyCount = _settingsService.Content.AvailableCurrencyCount;
            selectedFetchingIntervalMin = _settingsService.Content.FetchingIntervalMin;
            selectedFormatOfSaving = _settingsService.Content.FormatOfSaving;

            ApplyChangesCommand = new RelayCommand(async () =>
            {
                await _settingsService.SaveAsync(_settingsService.Content);
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
