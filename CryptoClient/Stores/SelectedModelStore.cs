using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Stores
{
    internal class SelectedModelStore
    {
        private readonly CryptoClientStore _cryptoClientStore;

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

        public SelectedModelStore(CryptoClientStore cryptoClientStore)
        {
            _cryptoClientStore = cryptoClientStore;
            _cryptoClientStore.CurrencyUpdated += _cryptoClientStore_CurrencyUpdated;
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
