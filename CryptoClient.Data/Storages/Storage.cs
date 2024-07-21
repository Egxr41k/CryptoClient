using CryptoClient.Data.Serializers;
using CryptoClient.Data.Services;
using CryptoClient.Logging;

namespace CryptoClient.Data.Storages
{
    public abstract class Storage<T>
    {
        private readonly ISerializer<T> _serializer;
        private readonly LoggingService _loggingService;
        private readonly string _storageFilePath;

        public T Content { get; protected set; }
        public Action ContentChanged;

        public Storage(
            ISerializer<T> serializer,
            LoggingService loggingService,
            string storageFilePath)
        {
            _serializer = serializer;
            _loggingService = loggingService;

            _storageFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                storageFilePath);
        }

        public void ClearStorage()
        {
            File.WriteAllText(_storageFilePath, "");
            _loggingService.WriteToLog("Storage cleared succesfully");
            ContentChanged.Invoke();
        }

        public string GetUnserializedData()
        {
            return File.ReadAllText(_storageFilePath);
        }

        public async Task<T?> ReadAsync()
        {
            if (!File.Exists(_storageFilePath))
            {
                return default;
            }

            try
            {
                return await _serializer.DeserializeAsync(_storageFilePath);
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLog($"An error occurred while Deserialing data: {ex.Message}");
                return default;
            }
        }

        public async Task SaveAsync(T data)
        {
            try
            {
                await _serializer.SerializeAsync(data, _storageFilePath);
                _loggingService.WriteToLog("Data succesfully serialized");
                ContentChanged.Invoke();
            }
            catch (Exception ex)
            {
                _loggingService.WriteToLog($"An error occurred while saving data: {ex.Message}");
            }
        }
    }
}
