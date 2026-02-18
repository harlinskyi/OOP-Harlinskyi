using System;
using lab25.Logging;

namespace lab25.Observers;

public class ProcessingLoggerObserver
{
    public void OnDataProcessed(object? sender, DataProcessedEventArgs e)
    {
        LoggerManager.Instance.Log($"Observer caught event. Result: {e.Result}");
    }
}