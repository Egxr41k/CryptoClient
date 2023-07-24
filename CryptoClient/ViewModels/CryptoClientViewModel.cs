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
        public ICommand SearchViewCommand { get; private set; }
        public ICommand ConvertViewCommand { get; private set; }
        
        public HomeViewModel HomeVM { get; }
        public SearchViewModel SearchVM { get; }
        public ConvertViewModel ConvertVM { get; }
        public ListingViewModel ListingVM { get; }
        public DetailsViewModel DetailsVM { get; }

        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }

        public CryptoClientViewModel(/*CryptoClientStore CryptoClientStore, SelectedModelStore selectedModelStore*/)
        {
            

            ListingVM = new ListingViewModel(/*CryptoClientStore, selectedModelStore*/);
            DetailsVM = new DetailsViewModel(/*selectedModelStore*/);
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
            ConvertVM = new ConvertViewModel();


            //AddListItemCommand = new OpenAddListItemCommand(listAppStore, modalNavigationStore);

            ListingVM.DetailsViewCommand = new RelayCommand(() =>
            {
                CurrentView = DetailsVM;
            });

            HomeViewCommand = new RelayCommand(() =>
            {
                CurrentView = HomeVM;
            });

            SearchViewCommand = new RelayCommand(() =>
            {
                CurrentView = SearchVM;
            });

            ConvertViewCommand = new RelayCommand(() =>
            {
                CurrentView = ConvertVM;
            });

            CurrentView = HomeVM;
        }
    }
}
