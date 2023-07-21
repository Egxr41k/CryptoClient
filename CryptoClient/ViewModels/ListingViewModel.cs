using CommunityToolkit.Mvvm.ComponentModel;
using CryptoClient.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using CryptoClient.Models;

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
        public string AppName = "CryptoClient";

        private async void CryptoListInitAsync()
        {
            var request = App.httpClient.GetAsync(
              "https://api.coincap.io/v2/assets").Result;
            string responce = await request.Content.ReadAsStringAsync();

            //JSON? restoredPerson = System.Text.Json.JsonSerializer.Deserialize<JSON>(responce);
            dynamic jobj = JObject.Parse(responce);
            dynamic jarr = (JArray)jobj.data;
            string[] cryptoName = new string[10];
            for (int i = 0; i < 10; i++)
            {
                cryptoName[i] = jarr[i].name.ToString();
                AddListItem(new CurrencyModel(cryptoName[i]));
            }

            //string? name = jarr[0].name;
            ////Console.WriteLine(name);

            //request = App.httpClient.GetAsync(
            //  $"https://api.coingecko.com/api/v3/coins/{name.ToLower()}").Result;
            //responce = request.Content.ReadAsStringAsync().Result;

            //jobj = JObject.Parse(responce);
            //Console.WriteLine(jobj.symbol);
        }

        public ListingViewModel(CryptoClientStore cryptoClientStore, SelectedModelStore selectedModelStore)
        {
            _cryptoClientStore = cryptoClientStore;
            _selectedModelStore = selectedModelStore;


            cryptoList = new ObservableCollection<ListingItemViewModel>();

            _cryptoClientStore.CurrencyUpdated += _CryptoClient_CurrencyUpdated;
            _cryptoClientStore.CurrencyAdded += _CryptoClient_CurrencyAdded;
            CryptoListInitAsync();
        }
        private void _CryptoClient_CurrencyUpdated(CurrencyModel model)
        {
            ListingItemViewModel? listingItemViewModel =
                cryptoList.FirstOrDefault(y => y.CurrencyModel.Id == model.Id);
        }

        private void _CryptoClient_CurrencyAdded(CurrencyModel model)
        {
            AddListItem(model);
        }

        private void AddListItem(CurrencyModel model)
        {
            //ICommand editCommand = new OpenEditListItemCommand(model, _modalNavigationStore);
            cryptoList.Add(
                new ListingItemViewModel(model, _cryptoClientStore));
        }
    }
}
