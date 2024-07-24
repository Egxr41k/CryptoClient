using System.Diagnostics;

namespace FinanceClient.Logging
{
    public class LoggingService
    {
        private readonly string _logFilePath;
        public string LogFileName { get; private set; }

        public Action ContentChanged;
        public LoggingService(string logFilePath = "log.txt")
        {
            LogFileName = logFilePath;

            _logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                logFilePath);

            if (!File.Exists(_logFilePath)) File.Create(_logFilePath);
        }

        public void OpenInFileExplorer()
        {
            // Ensure the file path is not null or empty
            if (string.IsNullOrEmpty(_logFilePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(_logFilePath));
            }

            // Use Process.Start to open the file explorer at the specified path
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{_logFilePath}\"",
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                };

                Process process = new Process
                {
                    StartInfo = processStartInfo
                };

                process.Start();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                Console.WriteLine($"An error occurred while trying to open the file explorer: {ex.Message}");
            }
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
