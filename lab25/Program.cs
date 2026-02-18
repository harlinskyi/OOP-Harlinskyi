using System;
using lab25.Logging;
using lab25.Processing;
using lab25.Observers;

namespace lab25;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // --- Сценарій 1: Повна інтеграція ---
        PrintHeader("[Сценарій 1]: Повна інтеграція");
        LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

        var context = new DataContext(new EncryptDataStrategy());
        var publisher = new DataPublisher();
        var observer = new ProcessingLoggerObserver();

        publisher.DataProcessed += observer.OnDataProcessed;

        string res1 = context.Execute("UserPassword123");
        publisher.Publish(res1);

        // --- Сценарій 2: Динамічна зміна логера ---
        PrintHeader("[Сценарій 2]: Зміна логера на File");
        LoggerManager.Instance.SetFactory(new FileLoggerFactory());

        string res2 = context.Execute("NewSessionData");
        publisher.Publish(res2);

        // --- Сценарій 3: Динамічна зміна стратегії ---
        PrintHeader("[Сценарій 3]: Зміна стратегії на Compress");
        context.SetStrategy(new CompressDataStrategy());

        string res3 = context.Execute("LargeImageFile.raw");
        publisher.Publish(res3);
    }

    static void PrintHeader(string title) =>
        Console.WriteLine($"\n{new string('-', 15)} {title} {new string('-', 15)}");
}