using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
