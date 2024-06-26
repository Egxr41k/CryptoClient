using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CryptoClient.Contracts;
using System.Globalization;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CryptoClient.Models;

namespace CryptoClient.ViewModels
{
    internal class ListingViewModel : ObservableObject
    {
        private CryptoClientStore _cryptoClientStore;
        private SelectedModelStore _selectedModelStore;
        private JsonService _jsonService;

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
                _selectedModelStore.SelectedModel = 
                    SelectedListingItemViewModel?.CurrencyModel;
                DetailsViewCommand.Execute(this);
            }
        }

        private async Task CryptoListInitAsync()
        {
            var models = await _jsonService.GetFullCurrenciesInfoAsync();
            foreach(var model in models) AddListItem(model);
        }

        public ListingViewModel(CryptoClientStore cryptoClientStore, SelectedModelStore selectedModelStore, JsonService jsonService)
        {
            _cryptoClientStore = cryptoClientStore;
            _selectedModelStore = selectedModelStore;
            _jsonService = jsonService;

            cryptoList = new ObservableCollection<ListingItemViewModel>();

            _cryptoClientStore.CurrencyUpdated +=
             CryptoClientStore_CurrencyUpdated;
            _cryptoClientStore.CurrencyAdded +=
             CryptoClientStore_CurrencyAdded;

            CryptoListInitAsync().GetAwaiter().GetResult();
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
                new ListingItemViewModel(model, _cryptoClientStore));
        }
    }
}
