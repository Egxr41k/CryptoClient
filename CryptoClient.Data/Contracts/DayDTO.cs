using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Contracts
{
    [DataContract]
    public class DayDTO
    {
        [DataMember(Name = "priceUsd")]
        public string PriceUsd { get; set; }

        [DataMember(Name = "time")]
        public long Time { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
