using System;

namespace lab24.Observers;

public class HistoryLoggerObserver
{
    public List<string> History { get; } = new List<string>();

    public void OnResultCalculated(double result, string opName)
    {
        History.Add($"{DateTime.Now:T}: {opName} = {result}");
    }
}