namespace CryptoClient.Logging
{
    public class LoggingService
    {
        private readonly string _logFilePath;
        public LoggingService(string logFilePath = "log.txt")
        {
            _logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                logFilePath);
        }

        public string GetLogs()
        {
            return File.ReadAllText(_logFilePath);
        }

        public void WriteLine(string message)
        {
            string format = "[{0:dd.MM.yy HH:mm:ss.fff}] | Thread: {1} | {2}\r\n";
            int threadId = Environment.CurrentManagedThreadId;

            string fullText = string.Format(format, DateTime.Now, threadId, message);

            File.AppendAllText(_logFilePath, fullText);
        }
    }
}
