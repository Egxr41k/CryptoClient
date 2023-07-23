using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient.Models
{
    internal class CurrencyModel
    {
        public Guid Id { get; private set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Link { get; set; }

        public Dictionary<DateTime, double> History { get; set; }

        public CurrencyModel(Guid id) { Id = id; }

        public async Task GetHistoryAsync()
        {
            var response = await App.httpClient.GetAsync(App.BASE_URL + Name + "/history?interval=d1");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);

            History = new Dictionary<DateTime, double>();
            foreach (JsonElement data in result.GetProperty("data").EnumerateArray())
            {
                long unixTime = data.GetProperty("time").GetInt64();

                DateTime date = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddMilliseconds(unixTime).ToLocalTime();

                string sdouble = data.GetProperty("priceUsd").GetString() ?? string.Empty;
                double value = Math.Round(double.Parse(sdouble, CultureInfo.InvariantCulture), 2);
                History.Add(date, value);
            }
        }
    }
}
