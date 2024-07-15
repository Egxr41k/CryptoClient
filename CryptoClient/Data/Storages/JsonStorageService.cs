using CryptoClient.Data.Services;
using CryptoClient.Models;
using CryptoClient.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CryptoClient.Data.Storages
{
    internal class JsonStorageService : IStorageService
    {
        private readonly string _storageFilePath;
        private readonly IJsonService _jsonService;

        private CurrencyModel[] CurrencyModels;

        public JsonStorageService(
            IJsonService jsonService,
            string storageFilePath = "storage.json")
        {
            _jsonService = jsonService;

            _storageFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                storageFilePath);
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public CurrencyModel[] Read()
        {
            throw new NotImplementedException();
        }

        public void Save(CurrencyModel[] currencyModels)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

}
