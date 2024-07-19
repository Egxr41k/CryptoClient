using System;
using CryptoClient.Data.Models;

namespace CryptoClient.Stores
{
    internal class CryptoClientStore
    {
        public event Action<CurrencyModel>? CurrencyAdded;
        public void Add(CurrencyModel model)
        {
            CurrencyAdded?.Invoke(model);
        }

        public event Action<CurrencyModel>? CurrencyUpdated;
        public void Update(CurrencyModel model)
        {
            CurrencyUpdated?.Invoke(model);
        }
    }
}
