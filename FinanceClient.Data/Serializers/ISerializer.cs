using FinanceClient.Data.Models;

namespace FinanceClient.Data.Serializers
{
    public interface ISerializer<T>
    {
        Task<T> DeserializeAsync(string path);
        Task SerializeAsync(T data, string path);
    }
}
