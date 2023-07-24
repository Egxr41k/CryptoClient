using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient
{
    public static class JsonService
    {
        private static HttpClient httpClient = new();
        private const string BASE_URL = "https://api.coincap.io/v2/assets/";
        public static async Task<Dictionary<DateTime, double>> GetHistoryAsync(string name)
        {
            var response = await httpClient.GetAsync(BASE_URL + name + "/history?interval=d1");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var History = new Dictionary<DateTime, double>();
            foreach (JsonElement data in result.GetProperty("data").EnumerateArray())
            {
                long unixTime = data.GetProperty("time").GetInt64();

                DateTime date = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddMilliseconds(unixTime).ToLocalTime();

                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                double value = Math.Round(double.Parse(sdouble, CultureInfo.InvariantCulture), 2);
                History.Add(date, value);
            }
            return History;
        }

        public static async Task<CurrencyModel?> SearchAsync(string name)
        {
            var response = await httpClient.GetAsync(BASE_URL + name + "/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(json);

                var data = result.GetProperty("data");
                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                return new CurrencyModel(Guid.NewGuid())
                {
                    Name = data.GetProperty("id").GetString() ?? string.Empty,
                    Symbol = data.GetProperty("symbol").GetString() ?? string.Empty,
                    Link = data.GetProperty("explorer").GetString() ?? string.Empty,
                    Price = double.Parse(sdouble, CultureInfo.InvariantCulture)
                };
            }
            else return null;
        }

        public static async Task<List<CurrencyModel>> GetTopCurrenciesAsync()
        {
            var response = await httpClient.GetAsync(BASE_URL);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var cryptoCurrencies = new List<CurrencyModel>();

            foreach (var data in result.GetProperty("data").EnumerateArray())
            {
                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                cryptoCurrencies.Add(new CurrencyModel(Guid.NewGuid())
                {
                    Name = data.GetProperty("id").GetString() ?? string.Empty,
                    Symbol = data.GetProperty("symbol").GetString() ?? string.Empty,
                    Link = data.GetProperty("explorer").GetString() ?? string.Empty,
                    Price = double.Parse(sdouble, CultureInfo.InvariantCulture)
                });

            }
            return cryptoCurrencies.OrderByDescending(c => c.Price).ToList();
        }
    }
}
