using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            JsonService jsonService)
        {
            ListingVM = new ListingViewModel(cryptoClientStore, selectedModelStore, jsonService);
            DetailsVM = new DetailsViewModel(selectedModelStore, jsonService);
            HomeVM = new HomeViewModel();

            ConvertVM = new ConvertViewModel(jsonService);

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
