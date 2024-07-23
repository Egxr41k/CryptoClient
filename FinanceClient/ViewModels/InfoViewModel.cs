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

        public InfoViewModel(
            CurrencyStorage storageService,
            LoggingService loggingService)
        {
            _storageService = storageService;
            _loggingService = loggingService;

            storageService.ContentChanged +=
                storageService_ContentChanged;

            loggingService.ContentChanged +=
            loggingService_ContentChanged;

            ClearStorageCommand = new RelayCommand(() =>
            _storageService.ClearStorage());

            ClearLogsCommand = new RelayCommand(() =>
                _loggingService.ClearLog());
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
