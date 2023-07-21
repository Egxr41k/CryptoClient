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

        private ListingItemViewModel _selectedlistingItem;

        public ListingItemViewModel SelectedListingItem
        {
            get { return _selectedlistingItem; }
            set
            {
                _selectedlistingItem = value;
                OnPropertyChanged();
                //_selectedModelStore.SelectedModel = _selectedListingItemViewModel?.SharpTorrentModel;
            }
        }

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
                cryptoName[i] = jarr[i].ToString();
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

        public ListingViewModel(CryptoClientStore _cryptoClientStore, SelectedModelStore _selectedModelStore)
        {
            this._cryptoClientStore = _cryptoClientStore;
            this._selectedModelStore = _selectedModelStore;


            cryptoList = new ObservableCollection<ListingItemViewModel>();
            CryptoListInitAsync();
        }
        private void _sharpTorrentStore_TorrentUpdated(CurrencyModel model)
        {
            ListingItemViewModel? listingItemViewModel =
                cryptoList.FirstOrDefault(y => y.CurrencyModel.Id == model.Id);
        }

        private void _sharpTorrentStore_TorrentAdded(CurrencyModel model)
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
