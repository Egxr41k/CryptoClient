using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Contracts
{
    [DataContract]
    public class HistoryResponse
    {
        [DataMember(Name = "data")]
        public DayDTO[] Data { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }
    }

    [DataContract]
    public class MarketListResponse
    {
        [DataMember(Name = "data")]
        public MarketDTO[] Data { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }
    }

    [DataContract]
    public class CurrencyResponse
    {
        [DataMember(Name = "data")]
        public CurrencyDTO Data { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }
    }

    [DataContract]
    public class CurrencyListResponse
    {
        [DataMember(Name = "data")]
        public CurrencyDTO[] Data { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }
    }

}
