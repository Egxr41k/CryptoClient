using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Stores
{
    internal static class SelectedModelStore
    {
        //private readonly CryptoClientStore _cryptoClientStore;

        private static CurrencyModel _selectedModel;
        public static CurrencyModel SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                SelectedModelChanged?.Invoke();
            }
        }


        public static Action SelectedModelChanged;


        //public SelectedModelStore(CryptoClientStore cryptoClientStore)
        //{
        //    _cryptoClientStore = cryptoClientStore;
        //    CryptoClientStore.CurrencyUpdated += _cryptoClientStore_CurrencyUpdated;
        //}
        public static void _cryptoClientStore_CurrencyUpdated(CurrencyModel model)
        {
            if (model.Id == SelectedModel?.Id)
            {
                SelectedModel = model;
            }
        }

    }
}
