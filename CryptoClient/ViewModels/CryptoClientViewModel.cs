using CommunityToolkit.Mvvm.ComponentModel;
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
        public string AppName = "CryptoClient";
        public DetailsViewModel DetailsViewModel { get; }
        public ListingViewModel ListingViewModel { get; }
        //public ICommand AddListItemCommand { get; }
        public CryptoClientViewModel(CryptoClientStore CryptoClientStore, SelectedModelStore selectedModelStore)
        {

            ListingViewModel = new ListingViewModel(CryptoClientStore, selectedModelStore);
            DetailsViewModel = new DetailsViewModel(selectedModelStore);

            //AddListItemCommand = new OpenAddListItemCommand(listAppStore, modalNavigationStore);
        }
    }
}
