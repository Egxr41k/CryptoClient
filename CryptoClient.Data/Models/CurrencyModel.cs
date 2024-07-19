using System.Xml.Serialization;

namespace CryptoClient.Data.Models
{
    public class CurrencyModel
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double ChangePercent { get; set; }
        public string Link { get; set; }

        [XmlIgnore]
        public Dictionary<DateTime, double> History { get; set; } = new Dictionary<DateTime, double>();

        [XmlIgnore]
        public Dictionary<string, double> Markets { get; set; } = new Dictionary<string, double>();

        [XmlElement("History")]
        public List<KeyValuePairStringDouble> HistorySerialized
        {
            get => History.Select(kv => new KeyValuePairStringDouble { Key = kv.Key.ToString("o"), Value = kv.Value }).ToList();
            set => History = value.ToDictionary(kv => DateTime.Parse(kv.Key), kv => kv.Value);
        }

        [XmlElement("Markets")]
        public List<KeyValuePairStringDouble> MarketsSerialized
        {
            get => Markets.Select(kv => new KeyValuePairStringDouble { Key = kv.Key, Value = kv.Value }).ToList();
            set => Markets = value.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }

    public class KeyValuePairStringDouble
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public double Value { get; set; }
    }
}
