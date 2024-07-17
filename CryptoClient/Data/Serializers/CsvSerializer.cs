using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                return new CurrencyModel
                {
                    Id = columns[0],
                    Symbol = columns[1],
                    Name = columns[2],
                    Price = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    ChangePercent = double.Parse(columns[4], CultureInfo.InvariantCulture),
                    Link = columns[5],
                    // Handling History and Markets fields if necessary
                };
            }).ToArray();
        }

        public async Task SerializeAsync(CurrencyModel[] data, string path)
        {
            var lines = new List<string> { "Id,Symbol,Name,Price,ChangePercent,Link,History,Markets" };
            lines.AddRange(data.Select(item =>
            {
                if (item is CurrencyModel model)
                {
                    var historyString = model.History != null
                        ? string.Join(";", model.History.Select(kv => $"{kv.Key:o}:{kv.Value}"))
                        : string.Empty;

                    var marketsString = model.Markets != null
                        ? string.Join(";", model.Markets.Select(kv => $"{kv.Key}:{kv.Value}"))
                        : string.Empty;

                    return $"{model.Id},{model.Symbol},{model.Name},{model.Price.ToString(CultureInfo.InvariantCulture)},{model.ChangePercent.ToString(CultureInfo.InvariantCulture)},{model.Link},{historyString},{marketsString}";
                }
                return string.Empty; // Add conversion logic for other types
            }));
            await File.WriteAllLinesAsync(path, lines);
        }
    }
}
