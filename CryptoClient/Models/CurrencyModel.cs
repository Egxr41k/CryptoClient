using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Models
{
    internal class CurrencyModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = "Currrency";
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public CurrencyModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;

        }
    }
}
