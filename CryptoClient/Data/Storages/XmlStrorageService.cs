using CryptoClient.Data.Services;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptoClient.Data.Storages
{
    internal class XmlStorageService : IStorageService
    {
        private readonly string _storageFilePath;
        private readonly IJsonService _jsonService;

        public XmlStorageService(IJsonService jsonService, string storageFilePath = "storage.xml")
        {
            _jsonService = jsonService;
            _storageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, storageFilePath);
        }

        public async Task<CurrencyModel[]> Read()
        {
            if (!File.Exists(_storageFilePath))
            {
                return await Update();
            }

            try
            {
                using (var stream = File.OpenRead(_storageFilePath))
                {
                    var serializer = new XmlSerializer(typeof(CurrencyModel[]));
                    var currencyModels = (CurrencyModel[])serializer.Deserialize(stream);
                    return currencyModels ?? await Update();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading data: {ex.Message}");
                return await Update();
            }
        }

        public void Save(CurrencyModel[] currencyModels)
        {
            try
            {
                using (var stream = File.Create(_storageFilePath))
                {
                    var serializer = new XmlSerializer(typeof(CurrencyModel[]));
                    serializer.Serialize(stream, currencyModels);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving data: {ex.Message}");
            }
        }

        public async Task<CurrencyModel[]> Update()
        {
            try
            {
                var currencyModels = await _jsonService.GetFullCurrenciesInfoAsync();
                Save(currencyModels);
                return currencyModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating data: {ex.Message}");
                throw;
            }
        }
    }
}
