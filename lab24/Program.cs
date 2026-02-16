using System;
using System.Collections.Generic;

using lab24.Strategies;
using lab24.Observers;
using lab24.Models;

namespace lab24;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Ініціалізація
        var publisher = new ResultPublisher();
        var processor = new NumericProcessor(new SquareOperationStrategy());

        // Створення та підписка спостерігачів
        var consoleLog = new ConsoleLoggerObserver();
        var historyLog = new HistoryLoggerObserver();
        var thresholdLog = new ThresholdNotifierObserver(100);

        publisher.ResultCalculated += consoleLog.OnResultCalculated;
        publisher.ResultCalculated += historyLog.OnResultCalculated;
        publisher.ResultCalculated += thresholdLog.OnResultCalculated;

        // Тестування різних стратегій
        double[] numbers = { 5, 12, 4 };

        foreach (var num in numbers)
        {
            double res = processor.Process(num);
            publisher.PublishResult(res, processor.GetCurrentStrategyName());
        }

        Console.WriteLine("\nЗміна стратегії на Куб...");
        processor.SetStrategy(new CubeOperationStrategy());
        double cubeRes = processor.Process(6);
        publisher.PublishResult(cubeRes, processor.GetCurrentStrategyName());

        // Вивід історії
        Console.WriteLine("\n--- Історія операцій ---");
        historyLog.History.ForEach(Console.WriteLine);
    }
}