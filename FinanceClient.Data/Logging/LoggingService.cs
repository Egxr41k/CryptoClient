namespace FinanceClient.Logging
{
    public class LoggingService
    {
        private readonly string _logFilePath;

        public Action ContentChanged;
        public LoggingService(string logFilePath = "log.txt")
        {
            _logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                logFilePath);

            if (!File.Exists(_logFilePath)) File.Create(_logFilePath);
        }

        public void ClearLog()
        {
            File.WriteAllText(_logFilePath, "");
            ContentChanged.Invoke();
        }

        public string GetLog()
        {
            return File.ReadAllText(_logFilePath);
        }

        public async Task WriteToLogAsync(string message)
        {
            string format = "[{0:dd.MM.yy HH:mm:ss.fff}] | Thread: {1} | {2}\r\n";
            int threadId = Environment.CurrentManagedThreadId;

            string fullText = string.Format(format, DateTime.Now, threadId, message);


            await File.AppendAllTextAsync(_logFilePath, fullText);

            ContentChanged?.Invoke();
        }
    }
}
