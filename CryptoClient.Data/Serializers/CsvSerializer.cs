using CryptoClient.Data.Models;
using System.Globalization;

namespace CryptoClient.Data.Serializers
{
    public class CsvSerializer : ISerializer
    {
        public async Task<CurrencyModel[]> DeserializeAsync(string path)
        {
            if (!File.Exists(path)) return Array.Empty<CurrencyModel>();
            var lines = await File.ReadAllLinesAsync(path) ?? Array.Empty<string>();
            return lines.Skip(1).Select(line =>
            {
                var columns = line.Split(',');

                var history = columns[6]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(item =>
                    {
                        var parts = item.Split(':');
                        return new KeyValuePair<DateTime, double>(
                            DateTime.Parse(parts[0], null, DateTimeStyles.RoundtripKind),
                            double.Parse(parts[1], CultureInfo.InvariantCulture)
                        );
                    })
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                var markets = columns[7]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(item =>
                    {
                        var parts = item.Split(':');
                        return new KeyValuePair<string, double>(
                            parts[0],
                            double.Parse(parts[1], CultureInfo.InvariantCulture)
                        );
                    })
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
        }

        public async Task SerializeAsync(CurrencyModel[] data, string path)
        {
            var lines = new List<string> { "Id,Symbol,Name,Price,ChangePercent,Link,History,Markets" };
            lines.AddRange(data.Select(model =>
            {
                var historyString = model.History != null
                    ? string.Join(";", model.History.Select(kv => $"{kv.Key:o}:{kv.Value}"))
                    : string.Empty;

                var marketsString = model.Markets != null
                    ? string.Join(";", model.Markets.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : string.Empty;

                return $"{model.Id},{model.Symbol},{model.Name},{model.Price.ToString(CultureInfo.InvariantCulture)},{model.ChangePercent.ToString(CultureInfo.InvariantCulture)},{model.Link},{historyString},{marketsString}";
            }));
            await File.WriteAllLinesAsync(path, lines);
        }
    }
}
