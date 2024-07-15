using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Contracts
{
    [DataContract]
    public class NbuCurrencyDTO
    {
        [DataMember(Name = "r030")]
        public int R030 { get; set; }  // Code of the currency

        [DataMember(Name = "txt")]
        public string Txt { get; set; }   // Full name of the currency

        [DataMember(Name = "rate")]
        public double Rate { get; set; }  // Exchange rate

        [DataMember(Name = "cc")]
        public string Cc { get; set; }    // Currency abbreviation

        [DataMember(Name = "exchangedate")]
        public string Exchangedate { get; set; }  // Date of the exchange rate
    }
}
