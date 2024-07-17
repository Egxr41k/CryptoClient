using CryptoClient.Data.Serializers;
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
    public class StorageService : IStorageService
    {
        private readonly ISerializer _serializer;
        private readonly string _storageFilePath;

        public StorageService(
            ISerializer serializer, 
            IJsonService jsonService, 
            string storageFilePath)
        {
            _serializer = serializer;
            _storageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, storageFilePath);
        }

        public async Task<CurrencyModel[]> ReadAsync()
        {
            if (!File.Exists(_storageFilePath))
            {
                return await UpdateAsync();
            }

            try
            {
                return await _serializer.DeserializeAsync<CurrencyModel>(_storageFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading data: {ex.Message}");
                return await UpdateAsync();
            }
        }

        public async Task SaveAsync(CurrencyModel[] data)
        {
            try
            {
                await _serializer.SerializeAsync(data, _storageFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving data: {ex.Message}");
            }
        }

        public async Task<CurrencyModel[]> UpdateAsync()
        {
            try
            {
                // Replace this with actual logic to fetch new data
                CurrencyModel[] newData = await Task.FromResult(Array.Empty<CurrencyModel>());
                await SaveAsync(newData);
                return newData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating data: {ex.Message}");
                throw;
            }
        }
    }
}
