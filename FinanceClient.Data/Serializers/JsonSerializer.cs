using FinanceClient.Data.Models;
using System.Text.Json;

namespace FinanceClient.Data.Serializers
{
    public class JsonSerializer<T> : ISerializer<T>
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

        public async Task<T> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return default;
            string json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public async Task SerializeAsync(T data, string path)
        {
            string json = JsonSerializer.Serialize(data, _options);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
