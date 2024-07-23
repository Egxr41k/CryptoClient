using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceClient.Data.Models;
using FinanceClient.Data.Storages;
using FinanceClient.Logging;
using FinanceClient.Settings;
using FinanceClient.Stores;

namespace FinanceClient.ViewModels
{
    internal class FinanceClientViewModel : ObservableObject
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

        public FinanceClientViewModel(
            FinanceClientStore cryptoClientStore, 
            SelectedModelStore selectedModelStore,
            CurrencyStorage storageService,
            SettingsStorage settingsService,
            LoggingService loggingService)
        {
            ListingVM = new ListingViewModel(
                cryptoClientStore, 
                selectedModelStore, 
                storageService, 
                settingsService);

            DetailsVM = new DetailsViewModel(
                selectedModelStore);

            var infoViewModel = new InfoViewModel(storageService, loggingService);

            SettingsVM = new SettingsViewModel(
                settingsService,
                infoViewModel);


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
