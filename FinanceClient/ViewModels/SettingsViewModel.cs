using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceClient.Settings;

namespace FinanceClient.ViewModels
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
                SetProperty(ref selectedFetchingIntervalMin, value);
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

        private string customCode;
        public string CustomCode
        {
            get => customCode;
            set
            {
                SetProperty(ref customCode, value);
                _settingsService.SetCustomControlCode(value);
            }
        }
        public List<string> CustomCodeList { get; }

        public SettingsViewModel(
            SettingsStorage settingsService,
            InfoViewModel infoViewModel)
        {
            _settingsService = settingsService;
            InfoViewModel = infoViewModel;

            UsedApiList = settingsService.UsedApiList;

            AvailableCurrencyCountList = settingsService.AvailableCurrencyCountOptions;

            FetchingIntervalMinList = settingsService.FetchingIntervalMinOptions;

            FormatOfSavingList = settingsService.FormatOfSavingOptions;

            CustomCodeList = settingsService.CustomControlCodes.Keys.ToList();
            _settingsService_ContentChanged();

            settingsService.ContentChanged += _settingsService_ContentChanged;

            ApplyChangesCommand = new RelayCommand(async () =>
            {
                await _settingsService.SaveAsync(_settingsService.Content);
                RestartApplication();
            });
        }

        private void _settingsService_ContentChanged()
        {
            SelectedUsedApi = _settingsService.Content.UsedApi;
            SelectedAvailableCurrencyCount = _settingsService.Content.AvailableCurrencyCount;
            SelectedFetchingIntervalMin = _settingsService.Content.FetchingIntervalMin;
            SelectedFormatOfSaving = _settingsService.Content.FormatOfSaving;
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
