using System;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RequestTester // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static HttpClient httpClient = new();
        static async Task Main(string[] args)
        {

            var request = httpClient.GetAsync(
              "https://api.coincap.io/v2/assets").Result;
            string responce = await request.Content.ReadAsStringAsync();

            //JSON? restoredPerson = System.Text.Json.JsonSerializer.Deserialize<JSON>(responce);
            dynamic jobj = JObject.Parse(responce);
            dynamic jarr = (JArray)jobj.data;

            string? name = jarr[0].name;
            name = name.ToLower();
            Console.WriteLine(name);

            request = httpClient.GetAsync(
              $"https://api.coingecko.com/api/v3/coins/{name}").Result;
            responce = request.Content.ReadAsStringAsync().Result;

            jobj = JObject.Parse(responce);
            Console.WriteLine(jobj.symbol);
        }
    }
}

record JSON(CryptoInfo[] data, long timestamp);
record CryptoInfo(
    string id,
    string rank,
    string symbol,
    string name,
    string supply,
    string maxSupply,
    string marketCapUsd,
    string volumeUsd24Hr,
    string changePercent24Hr,
    string vwap24Hr,
    string explorer
    );
