using System;

namespace lab25.Logging;

public class FileLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"[FileLog - Simulation]: Writing to file: {message}");
}