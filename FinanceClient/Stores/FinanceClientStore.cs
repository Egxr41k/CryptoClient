using System;
using FinanceClient.Data.Models;

namespace FinanceClient.Stores
{
    internal class FinanceClientStore
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
