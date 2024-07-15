using CryptoClient.Data.Services;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Storages
{
    internal interface IStorageService
    {
        void Init();
        void Update();
        CurrencyModel[] Read();
        void Save(CurrencyModel[] currencyModels);
    }
}
