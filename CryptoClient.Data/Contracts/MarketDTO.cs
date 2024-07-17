using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Contracts
{
    [DataContract]
    public class MarketDTO
    {
        [DataMember(Name = "exchangeId")]
        public string ExchangeId { get; set; }

        [DataMember(Name = "baseId")]
        public string BaseId { get; set; }

        [DataMember(Name = "quoteId")]
        public string QuoteId { get; set; }

        [DataMember(Name = "baseSymbol")]
        public string BaseSymbol { get; set; }

        [DataMember(Name = "quoteSymbol")]
        public string QuoteSymbol { get; set; }

        [DataMember(Name = "volumeUsd24Hr")]
        public string VolumeUsd24Hr { get; set; }

        [DataMember(Name = "priceUsd")]
        public string PriceUsd { get; set; }

        [DataMember(Name = "volumePercent")]
        public string VolumePercent { get; set; }
    }
}
