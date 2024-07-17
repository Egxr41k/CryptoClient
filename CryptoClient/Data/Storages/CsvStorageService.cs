using CryptoClient.Data.Services;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoClient.Data.Storages
{
    internal class CsvStorageService : IStorageService
    {
        private readonly string _storageFilePath;
        private readonly IJsonService _jsonService;

        public CsvStorageService(IJsonService jsonService, string storageFilePath = "storage.csv")
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
                var lines = await File.ReadAllLinesAsync(_storageFilePath);
                var currencyModels = lines.Skip(1).Select(line =>
                {
                    var columns = line.Split(',');

                    // Parse History dictionary
                    var history = columns[6]
                        .Split(';')
                        .Select(pair => pair.Split(':'))
                        .ToDictionary(
                            kvp => DateTime.Parse(kvp[0]),
                            kvp => double.Parse(kvp[1], CultureInfo.InvariantCulture)
                        );

                    // Parse Markets dictionary
                    var markets = columns[7]
                        .Split(';')
                        .Select(pair => pair.Split(':'))
                        .ToDictionary(
                            kvp => kvp[0],
                            kvp => double.Parse(kvp[1], CultureInfo.InvariantCulture)
                        );

                    return new CurrencyModel
                    {
                        Id = columns[0],
                        Symbol = columns[1],
                        Name = columns[2],
                        Price = double.Parse(columns[3], CultureInfo.InvariantCulture),
                        ChangePercent = double.Parse(columns[4], CultureInfo.InvariantCulture),
                        Link = columns[5],
                        History = history,
                        Markets = markets
                    };
                }).ToArray();

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
                var lines = new List<string>
                {
                    "Id,Symbol,Name,Price,ChangePercent,Link,History,Markets"
                };

                lines.AddRange(currencyModels.Select(model =>
                    $"{model.Id},{model.Symbol},{model.Name},{model.Price.ToString(CultureInfo.InvariantCulture)},{model.ChangePercent.ToString(CultureInfo.InvariantCulture)},{model.Link}," +
                    $"{string.Join(";", model.History.Select(kvp => $"{kvp.Key.ToShortDateString()}:{kvp.Value.ToString(CultureInfo.InvariantCulture)}"))}," +
                    $"{string.Join(";", model.Markets.Select(kvp => $"{kvp.Key}:{kvp.Value.ToString(CultureInfo.InvariantCulture)}"))}"
                ));

                File.WriteAllLines(_storageFilePath, lines);
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
