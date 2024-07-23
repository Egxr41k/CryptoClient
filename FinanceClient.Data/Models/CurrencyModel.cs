using System.Xml.Serialization;

namespace FinanceClient.Data.Models
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

        [XmlArray("History")]
        [XmlArrayItem("KeyValuePairStringDouble")]
        public KeyValuePairStringDouble[] HistorySerialized
        {
            get => History.Select(kv => new KeyValuePairStringDouble { Key = kv.Key.ToString("o"), Value = kv.Value.ToString() }).ToArray();
            set => History = value.ToDictionary(kv => DateTime.Parse(kv.Key), kv => double.Parse(kv.Value));
        }

        [XmlArray("Markets")]
        [XmlArrayItem("KeyValuePairStringDouble")]
        public KeyValuePairStringDouble[] MarketsSerialized
        {
            get => Markets.Select(kv => new KeyValuePairStringDouble { Key = kv.Key, Value = kv.Value.ToString() }).ToArray();
            set => Markets = value.ToDictionary(kv => kv.Key, kv => double.Parse(kv.Value));
        }
    }

    public class KeyValuePairStringDouble
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}
