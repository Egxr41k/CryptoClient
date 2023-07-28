using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CryptoClient.Models;
using System.Globalization;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace CryptoClient.ViewModels
{
    internal class ListingViewModel : ObservableObject
    {
        //private CryptoClientStore _cryptoClientStore;
        //private SelectedModelStore _selectedModelStore;
        public RelayCommand DetailsViewCommand;
        public IEnumerable<ListingItemViewModel> CryptoList => cryptoList;
        private readonly ObservableCollection<ListingItemViewModel> cryptoList;

        private ListingItemViewModel _selectedListingItemViewModel;
        public ListingItemViewModel SelectedListingItemViewModel
        {
            get { return _selectedListingItemViewModel; }
            set
            {
                SetProperty(ref _selectedListingItemViewModel, value);
                //_selectedModelStore.SelectedModel = SelectedListingItemViewModel?.CurrencyModel;
                SelectedModelStore.SelectedModel = SelectedListingItemViewModel?.CurrencyModel;
                DetailsViewCommand.Execute(this);
            }
        }

        private async void CryptoListInitAsync()
        {
            var models = JsonService.GetTopCurrenciesAsync().Result;
            for (int i = 0; CryptoList.Count() < 10; i++)
            {
                if (i != 2 && i != 1)
                {
                     models[i].History =
                        await JsonService.GetHistoryAsync(models[i].Name);
                     models[i].Markets =
                        await JsonService.GetMarketsAsync(models[i].Name);
                    if (models[i].History != null) AddListItem(models[i]);
                }
            }
        }

        public ListingViewModel(/*CryptoClientStore cryptoClientStore, SelectedModelStore selectedModelStore*/)
        {
            //_cryptoClientStore = cryptoClientStore;
            //_selectedModelStore = selectedModelStore;


            cryptoList = new ObservableCollection<ListingItemViewModel>();

            //_cryptoClientStore.CurrencyUpdated +=
            // CryptoClientStore_CurrencyUpdated;
            //_cryptoClientStore.CurrencyAdded +=
            // CryptoClientStore_CurrencyAdded;

            CryptoClientStore.CurrencyUpdated +=
            CryptoClientStore_CurrencyUpdated;
            CryptoClientStore.CurrencyAdded += 
            CryptoClientStore_CurrencyAdded;


            CryptoListInitAsync();
        }
        private void CryptoClientStore_CurrencyUpdated(CurrencyModel model)
        {
            ListingItemViewModel? listingItemViewModel =
                cryptoList.FirstOrDefault(y => y.CurrencyModel.Id == model.Id);
        }

        private void CryptoClientStore_CurrencyAdded(CurrencyModel model)
        {
            AddListItem(model);
        }

        private void AddListItem(CurrencyModel model)
        {
            cryptoList.Add(
                new ListingItemViewModel(model/*, _cryptoClientStore*/));
        }
    }
}
