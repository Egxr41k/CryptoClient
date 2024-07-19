using CryptoClient.Data.Models;

namespace CryptoClient.Data.Storages
{
    public interface IStorageService
    {
        Task<CurrencyModel[]> UpdateAsync();
        Task<CurrencyModel[]> ReadAsync();
        Task SaveAsync(CurrencyModel[] data);
    }
}
