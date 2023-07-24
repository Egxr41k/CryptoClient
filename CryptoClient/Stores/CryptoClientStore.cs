using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Stores
{
    internal static class CryptoClientStore
    {
        public static event Action<CurrencyModel>? CurrencyAdded;
        public static void Add(CurrencyModel model)
        {
            CurrencyAdded?.Invoke(model);
        }

        public static event Action<CurrencyModel>? CurrencyUpdated;
        public static void Update(CurrencyModel model)
        {
            CurrencyUpdated?.Invoke(model);
        }
    }
}
