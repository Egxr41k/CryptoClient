using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Serializers
{
    public interface ISerializer
    {
        Task<CurrencyModel[]> DeserializeAsync(string path);
        Task SerializeAsync(CurrencyModel[] data, string path);
    }
}
