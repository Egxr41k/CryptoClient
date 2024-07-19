using CryptoClient.Data.Models;

namespace CryptoClient.Data.Serializers
{
    public interface ISerializer
    {
        Task<CurrencyModel[]> DeserializeAsync(string path);
        Task SerializeAsync(CurrencyModel[] data, string path);
    }
}
