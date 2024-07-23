using System;
using FinanceClient.Data.Models;

namespace FinanceClient.Stores
{
    internal class SelectedModelStore
    {
        private readonly FinanceClientStore _cryptoClientStore;

        public Action SelectedModelChanged;

        private CurrencyModel _selectedModel;
        public CurrencyModel SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                SelectedModelChanged?.Invoke();
            }
        }

        public SelectedModelStore(FinanceClientStore cryptoClientStore)
        {
            _cryptoClientStore = cryptoClientStore;
            _cryptoClientStore.CurrencyUpdated +=
                _cryptoClientStore_CurrencyUpdated;
        }

        public void _cryptoClientStore_CurrencyUpdated(CurrencyModel model)
        {
            if (model.Id == SelectedModel?.Id)
            {
                SelectedModel = model;
            }
        }
    }
}
