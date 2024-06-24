using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient
{
    public class JsonService
    {
        private HttpClient _httpClient;
        private string _baseUrl;

        public JsonService(string baseUrl = "https://api.coincap.io/v2/assets/")
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string name)
        {
            var response = await _httpClient.GetAsync(_baseUrl + name + "/history?interval=d1");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var history = new Dictionary<DateTime, double>();
            foreach (JsonElement data in result.GetProperty("data").EnumerateArray())
            {
                long unixTime = data.GetProperty("time").GetInt64();

                DateTime date = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddMilliseconds(unixTime).ToLocalTime();

                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                double value = Math.Round(double.Parse(sdouble, CultureInfo.InvariantCulture), 2);
                history.Add(date, value);
            }
            return history;
        }

        public async Task<Dictionary<string, double>> GetMarketsAsync(string name)
        {
            var response = await _httpClient.GetAsync(_baseUrl + name + "/markets");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var markets = new Dictionary<string, double>();
            foreach (JsonElement data in result.GetProperty("data").EnumerateArray())
            {
                string sprice = data.GetProperty("priceUsd").GetString();
                double value = Math.Round(double.Parse(sprice, CultureInfo.InvariantCulture), 2);
                string key = data.GetProperty("exchangeId").GetString();

                if (!markets.Keys.Contains(key)) markets.Add(data.GetProperty("exchangeId").GetString(), value);
            }

            return markets
                .OrderByDescending(market => market.Value)
                .Take(5)
                .ToDictionary(x => x.Key, x=> x.Value);
        }

        public async Task<CurrencyModel?> SearchAsync(string name)
        {
            var response = await _httpClient.GetAsync(_baseUrl + name + "/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(json);
                return GetModel(result.GetProperty("data"));
            } else return null;
        }

        public async Task<List<CurrencyModel>> GetTopCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            var cryptoCurrencies = new List<CurrencyModel>();

            foreach (var data in result.GetProperty("data").EnumerateArray())
            {
                cryptoCurrencies.Add(GetModel(data));
            }
            return cryptoCurrencies.OrderByDescending(c => c.Price).ToList();
        }

        private CurrencyModel GetModel(JsonElement data)
        {
            string price = data.GetProperty("priceUsd").GetString() ?? string.Empty;
            string changePercent = data.GetProperty("changePercent24Hr").GetString() ?? string.Empty;

            return new CurrencyModel()
            {
                Id = data.GetProperty("id").GetString() ?? string.Empty,
                Name = data.GetProperty("name").GetString() ?? string.Empty,
                Symbol = data.GetProperty("symbol").GetString() ?? string.Empty,
                Link = data.GetProperty("explorer").GetString() ?? string.Empty,
                Price = Math.Round(double.Parse(price, CultureInfo.InvariantCulture), 2),
                ChangePercent = Math.Round(double.Parse(changePercent, CultureInfo.InvariantCulture), 2)
            };
        }
    }
}
