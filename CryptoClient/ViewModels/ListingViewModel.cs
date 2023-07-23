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

namespace CryptoClient.ViewModels
{
    internal class ListingViewModel : ObservableObject
    {
        private CryptoClientStore _cryptoClientStore; //_selectedlistingItem
        private SelectedModelStore _selectedModelStore;

        public IEnumerable<ListingItemViewModel> CryptoList => cryptoList;
        private readonly ObservableCollection<ListingItemViewModel> cryptoList;

        private ListingItemViewModel _selectedListingItemViewModel;
        public ListingItemViewModel SelectedListingItemViewModel
        {
            get { return _selectedListingItemViewModel; }
            set
            {
                SetProperty(ref _selectedListingItemViewModel, value);
                _selectedModelStore.SelectedModel = SelectedListingItemViewModel?.CurrencyModel;
            }
        }

        private async void CryptoListInitAsync()
        {
            var models = GetTopCurrenciesAsync().Result;
            for (int i = 0; CryptoList.Count() < 10; i++)
            {
                if (i != 2 && i != 1)
                {
                     models[i].GetHistoryAsync().Wait();
                    if (models[i].History != null) AddListItem(models[i]);
                }
            }
        }

        private async Task<List<CurrencyModel>> GetTopCurrenciesAsync()
        {
            var response = await App.httpClient.GetAsync(App.BASE_URL);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var cryptoCurrencies = new List<CurrencyModel>();

            foreach (var data in result.GetProperty("data").EnumerateArray())
            {
                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                cryptoCurrencies.Add(new CurrencyModel(Guid.NewGuid())
                {
                    Name = data.GetProperty("id").GetString() ?? string.Empty,
                    Symbol = data.GetProperty("symbol").GetString() ?? string.Empty,
                    Link = data.GetProperty("explorer").GetString() ?? string.Empty,
                    Price = double.Parse(sdouble, CultureInfo.InvariantCulture)
                });
                
            }
            return cryptoCurrencies.OrderByDescending(c => c.Price).ToList();
        }


        public ListingViewModel(CryptoClientStore cryptoClientStore, SelectedModelStore selectedModelStore)
        {
            _cryptoClientStore = cryptoClientStore;
            _selectedModelStore = selectedModelStore;


            cryptoList = new ObservableCollection<ListingItemViewModel>();

            _cryptoClientStore.CurrencyUpdated +=
             CryptoClientStore_CurrencyUpdated;
            _cryptoClientStore.CurrencyAdded +=
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
                new ListingItemViewModel(model, _cryptoClientStore));
        }
    }
}
