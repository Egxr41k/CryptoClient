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
        public Guid Id { get; private set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Link { get; set; }

        public Dictionary<DateTime, double> History { get; set; }

        public CurrencyModel(Guid id) { Id = id; }

     
    }
}
