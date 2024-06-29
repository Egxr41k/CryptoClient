using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Settings
{
    public class SettingsDTO
    {
        public string UsedApi { get; set; }
        public int AvailableCurrencyCount { get; set; }
        public int FetchingIntervalMin { get; set; }
        public string FormatOfSaving { get; set; }
    }
}
