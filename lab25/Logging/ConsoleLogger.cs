using System;

namespace lab25.Logging;

public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"[ConsoleLog]: {message}");
}