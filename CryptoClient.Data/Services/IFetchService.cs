using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoClient.Data.Services
{
    public interface IFetchService
    {
        Task<T> FetchDataAsync<T>(string url);
    }
}
