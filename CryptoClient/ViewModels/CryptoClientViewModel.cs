using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Data.Storages;
using CryptoClient.Settings;
using CryptoClient.Stores;

namespace CryptoClient.ViewModels
{
    internal class CryptoClientViewModel : ObservableObject
    {
        public string AppName { get; set; } = "CryptoClient";
        
        public ICommand HomeViewCommand { get; private set; }
        public ICommand ConvertViewCommand { get; private set; }
        
        public HomeViewModel HomeVM { get; }
        public ConvertViewModel ConvertVM { get; }
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
            IStorageService storageService,
            SettingsService settingsService)
        {
            ListingVM = new ListingViewModel(cryptoClientStore, selectedModelStore, storageService, settingsService);
            DetailsVM = new DetailsViewModel(selectedModelStore, storageService);
            HomeVM = new HomeViewModel(settingsService);

            //ConvertVM = new ConvertViewModel(_apiService);

            ListingVM.DetailsViewCommand = new RelayCommand(() =>
            {
                CurrentView = DetailsVM;
            });

            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = HomeVM;
            });

            ConvertViewCommand = new RelayCommand(() =>
            {
                CurrentView = ConvertVM;
            });

            CurrentView = HomeVM;
        }
    }
}
