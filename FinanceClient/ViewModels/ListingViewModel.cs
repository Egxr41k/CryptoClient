using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceClient.Stores;
using FinanceClient.Settings;
using FinanceClient.Data.Storages;
using FinanceClient.Data.Models;
using System.Threading.Tasks;

namespace FinanceClient.ViewModels
{
    internal class ListingViewModel : ObservableObject
    {
        private FinanceClientStore _cryptoClientStore;
        private SelectedModelStore _selectedModelStore;
        private CurrencyStorage _strorageService;

        public RelayCommand DetailsViewCommand;
        public IEnumerable<ListingItemViewModel> CryptoList => cryptoList;
        private readonly ObservableCollection<ListingItemViewModel> cryptoList;

        private ListingItemViewModel _selectedListingItemViewModel;
        public ListingItemViewModel SelectedListingItemViewModel
        {
            get { return _selectedListingItemViewModel; }
            set
            {
                if (value == null) return;
                SetProperty(ref _selectedListingItemViewModel, value);
                _selectedModelStore.SelectedModel = 
                    SelectedListingItemViewModel?.CurrencyModel;
                DetailsViewCommand.Execute(this);
            }
        }

        private void OverwriteCryptoList(CurrencyModel[] models)
        {
            if (models == null || models.Length == 0) return;
            cryptoList.Clear();
            foreach (var model in models) AddListItem(model);
        }

        public ListingViewModel(
            FinanceClientStore cryptoClientStore, 
            SelectedModelStore selectedModelStore,
            CurrencyStorage strorageService,
            SettingsStorage settingsService)
        {
            _cryptoClientStore = cryptoClientStore;
            _selectedModelStore = selectedModelStore;
            _strorageService = strorageService;

            cryptoList = new ObservableCollection<ListingItemViewModel>();

            _cryptoClientStore.CurrencyUpdated +=
             CryptoClientStore_CurrencyUpdated;
            _cryptoClientStore.CurrencyAdded +=
             CryptoClientStore_CurrencyAdded;

            int intervalMin = settingsService.Content.FetchingIntervalMin;
            int intervalSec = 10; //intervalMin * 60;
            int intervalMillisec = intervalSec * 1000;

            RefreshingLoop(intervalMillisec);
        }

        private void RefreshingLoop(int intervalMillisec)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var models = cryptoList.Count == 0 ?
                        await _strorageService.ReadAsync() ?? 
                            await _strorageService.UpdateAsync() :
                        await _strorageService.UpdateAsync();

                    if (models != null && models.Length != 0)
                    {
                        cryptoList.Clear();
                        foreach (var model in models) AddListItem(model);
                    }

                    await Task.Delay(intervalMillisec);
                }
            });
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
