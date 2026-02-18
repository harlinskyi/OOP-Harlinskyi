using System;

namespace lab25.Logging;

public class LoggerManager
{
    private static LoggerManager? _instance;
    private ILogger? _logger;
    private LoggerFactory? _factory;

    private LoggerManager() { }

    public static LoggerManager Instance => _instance ??= new LoggerManager();

    public void SetFactory(LoggerFactory factory)
    {
        _factory = factory;
        _logger = _factory.CreateLogger();
    }

    public void Log(string message) => _logger?.Log(message);
}