using System;

namespace lab24.Observers;

public class ConsoleLoggerObserver
{
    public void OnResultCalculated(double result, string opName)
    {
        Console.WriteLine($"[Log]: Операція '{opName}' завершена. Результат: {result:F2}");
    }
}