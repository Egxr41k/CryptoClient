using CryptoClient.Data.Models;
using System.Text.Json;

namespace CryptoClient.Data.Serializers
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _options;

        public JsonSerializer()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<CurrencyModel[]> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return Array.Empty<CurrencyModel>();
            var json = await File.ReadAllTextAsync(path);
            return System.Text.Json.JsonSerializer.Deserialize<CurrencyModel[]>(json, _options) ?? Array.Empty<CurrencyModel>();
        }

        public async Task SerializeAsync(CurrencyModel[] data, string path)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data, _options);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
