using FinanceClient.Data.Serializers;
using FinanceClient.Data.Services;
using FinanceClient.Logging;

namespace FinanceClient.Data.Storages
{
    public abstract class Storage<T>
    {
        private readonly ISerializer<T> _serializer;
        private readonly LoggingService _loggingService;
        private readonly string _storageFilePath;

        public string StorageFileName { get; private set; }
        public T Content { get; protected set; }
        public Action ContentChanged;

        public Storage(
            ISerializer<T> serializer,
            LoggingService loggingService,
            string storageFilePath)
        {
            StorageFileName = storageFilePath;
            _serializer = serializer;
            _loggingService = loggingService;

            _storageFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                storageFilePath);

            if (!File.Exists(_storageFilePath)) File.Create(_storageFilePath);
        }

        public void ClearStorage()
        {
            File.WriteAllText(_storageFilePath, "");
            _loggingService.WriteToLogAsync("Storage cleared succesfully");
            ContentChanged.Invoke();
        }

        public string GetUnserializedData()
        {
            return File.ReadAllText(_storageFilePath);
        }

        public async Task<T?> ReadAsync()
        {
            try
            {
                var data = await _serializer.DeserializeAsync(_storageFilePath);
                if (data == null)
                {
                    _loggingService.WriteToLogAsync($"No data read from {StorageFileName}");
                    return default;
                }
                else
                {
                    _loggingService.WriteToLogAsync($"Data read successfully from {StorageFileName}");
                    return data;
                }
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLogAsync($"Error fetching data from {StorageFileName}: {ex.Message}");
                return default;
            }
        }

        public async Task SaveAsync(T data)
        {
            try
            {
                await _serializer.SerializeAsync(data, _storageFilePath);
                _loggingService.WriteToLogAsync($"Data saved succesfully to {StorageFileName}");
                ContentChanged?.Invoke();
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLogAsync($"An error occurred while saving data: {ex.Message}");
            }
        }
    }
}
