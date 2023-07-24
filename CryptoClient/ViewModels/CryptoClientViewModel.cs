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
        public DetailsViewModel DetailsViewModel { get; }
        public ICommand HomeViewCommand { get; private set; }
        public ListingViewModel ListingViewModel { get; }
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }
        //public ICommand AddListItemCommand { get; }
        public CryptoClientViewModel(/*CryptoClientStore CryptoClientStore, SelectedModelStore selectedModelStore*/)
        {
            CurrentView = App.HomeVM;

            ListingViewModel = new ListingViewModel(/*CryptoClientStore, selectedModelStore*/);
            DetailsViewModel = new DetailsViewModel(/*selectedModelStore*/);

            //AddListItemCommand = new OpenAddListItemCommand(listAppStore, modalNavigationStore);

            ListingViewModel.DetailsViewCommand = new RelayCommand(() =>
            {
                CurrentView = DetailsViewModel;
            });

            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = App.HomeVM;
            });

        }
    }
}
