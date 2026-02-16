using System;

namespace lab24.Observers;

public class ThresholdNotifierObserver
{
    private readonly double _threshold;
    public ThresholdNotifierObserver(double threshold) => _threshold = threshold;

    public void OnResultCalculated(double result, string opName)
    {
        if (result > _threshold)
        {
            Console.WriteLine($"[Warn]: Результат {result:F2} перевищив поріг {_threshold}!");
        }
    }
}