using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient.Models
{
    public class CurrencyModel
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double ChangePercent { get; set; }
        public string Link { get; set; }
        public Dictionary<DateTime, double> History { get; set; }
        public Dictionary<string, double> Markets { get; set; }
    }
}
