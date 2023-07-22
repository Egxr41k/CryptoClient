using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoClient.Models
{
    internal class CurrencyModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = "Currrency";
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string Link { get; set; } = string.Empty;

        public Dictionary<DateTime, int> History { get; set; }

        public List<int> Prices = new();
        public List<DateTime> Dates = new();

        public CurrencyModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            GetCurrencyHistory();
        }

        private void GetCurrencyHistory()
        {
            var request = App.httpClient.GetAsync(
              $"https://api.coincap.io/v2/assets/{Name.ToLower()}/history?interval=d1").Result;
            string responce = request.Content.ReadAsStringAsync().Result;

            dynamic jobj = JObject.Parse(responce);
            dynamic jarr = (JArray)jobj.data;
            if (jarr == null){ return; }

            Prices = new();
            Dates = new();
            History = new Dictionary<DateTime, int>();

            for (int i = 0; i < jarr.Count; i++)
            {
                string string1 = jarr[i].priceUsd.ToString();
                int price = Convert.ToInt32(string1.Split(".")[0]);
                
                string string2 = jarr[i].time.ToString();
                long unixTime = long.Parse(string2);

                DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddMilliseconds(unixTime).ToLocalTime();

                Prices.Add(price);
                Dates.Add(dateTime);
                History.Add(dateTime, price);
            }
        }
    }
}
