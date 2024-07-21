using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Data.Storages;
using CryptoClient.Logging;
using CryptoClient.Settings;
using CryptoClient.Stores;

namespace CryptoClient.ViewModels
{
    internal class CryptoClientViewModel : ObservableObject
    {        
        public ICommand SettingsViewCommand { get; private set; }
        
        public SettingsViewModel SettingsVM { get; }
        public ListingViewModel ListingVM { get; }
        public DetailsViewModel DetailsVM { get; }

        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }

        public CryptoClientViewModel(
            CryptoClientStore cryptoClientStore, 
            SelectedModelStore selectedModelStore,
            StorageService storageService,
            SettingsService settingsService,
            LoggingService loggingService)
        {
            ListingVM = new ListingViewModel(
                cryptoClientStore, 
                selectedModelStore, 
                storageService, 
                settingsService);

            DetailsVM = new DetailsViewModel(
                selectedModelStore, 
                storageService);

            SettingsVM = new SettingsViewModel(
                settingsService,
                storageService,
                loggingService);

            ListingVM.DetailsViewCommand = new RelayCommand(() =>
            {
                CurrentView = DetailsVM;
            });

            SettingsViewCommand = new RelayCommand(() =>
            {
                CurrentView = SettingsVM;
            });

            CurrentView = SettingsVM;
        }
    }
}
