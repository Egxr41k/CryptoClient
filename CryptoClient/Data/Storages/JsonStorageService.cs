using CryptoClient.Data.Services;
using CryptoClient.Models;
using CryptoClient.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CryptoClient.Data.Storages
{
    internal class JsonStorageService : IStorageService
    {
        private readonly string _storageFilePath;
        private readonly IJsonService _jsonService;

        public JsonStorageService(
            IJsonService jsonService,
            string storageFilePath = "storage.json")
        {
            _jsonService = jsonService;

            _storageFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                storageFilePath);
        }

        public async Task<CurrencyModel[]> Read()
        {
            if (!File.Exists(_storageFilePath))
            {
                return await Update();
            }

            try
            {
                string jsonString = File.ReadAllText(_storageFilePath);
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var currencyModels = JsonSerializer.Deserialize<CurrencyModel[]>(jsonString, jsonOptions);
                return currencyModels ?? await Update();
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
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(currencyModels, jsonOptions);
                File.WriteAllText(_storageFilePath, jsonString);
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
                Console.WriteLine($"An error occurred while saving data: {ex.Message}");
                throw;
            }
            
        }
    }
}
