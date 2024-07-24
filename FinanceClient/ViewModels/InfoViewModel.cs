using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceClient.Data.Storages;
using FinanceClient.Logging;

namespace FinanceClient.ViewModels
{
    public class InfoViewModel : ObservableObject
    {
        private readonly LoggingService _loggingService;
        private readonly CurrencyStorage _storageService;

        public ICommand ClearStorageCommand { get; set; }
        public ICommand ClearLogsCommand { get; set; }
        public ICommand OpenStorageCommand { get; set; }
        public ICommand OpenLogsCommand { get; set; }

        public string LogFileName { get; }

        public string log;
        public string Log
        {
            get => log;
            set => SetProperty(ref log, value);
        }

        public string StorageFileName { get; }

        private string unserializedData;
        public string UnserializedData
        {
            get => unserializedData;
            set => SetProperty(ref unserializedData, value);
        }

        public InfoViewModel(
            CurrencyStorage storageService,
            LoggingService loggingService)
        {
            _storageService = storageService;
            _loggingService = loggingService;

            StorageFileName = _storageService.StorageFileName;
            LogFileName = _loggingService.LogFileName;

            storageService.ContentChanged +=
                storageService_ContentChanged;

            loggingService.ContentChanged +=
            loggingService_ContentChanged;

            ClearStorageCommand = new RelayCommand(() =>
                _storageService.ClearStorage());

            OpenStorageCommand = new RelayCommand(() =>
                _storageService.OpenInFileExplorer());

            ClearLogsCommand = new RelayCommand(() =>
                _loggingService.ClearLog());

            OpenLogsCommand = new RelayCommand(() =>
                _loggingService.OpenInFileExplorer());
        }

        private void storageService_ContentChanged()
        {
            UnserializedData = _storageService.GetUnserializedData();
        }

        private void loggingService_ContentChanged()
        {
            Log = _loggingService.GetLog();
        }
    }
}
