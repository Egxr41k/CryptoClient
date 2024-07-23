using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FinanceClient.Data.Contracts
{
    [DataContract]
    public class NbuCurrencyDTO
    {
        [DataMember(Name = "r030")]
        public int R030 { get; set; }

        [DataMember(Name = "txt")]
        public string Txt { get; set; }

        [DataMember(Name = "rate")]
        public double Rate { get; set; }

        [DataMember(Name = "cc")]
        public string Cc { get; set; }

        [DataMember(Name = "exchangedate")]
        public string Exchangedate { get; set; }
    }
}
