using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6v7
{
    // Клас для зберігання температурних записів
    public class TemperatureRecord
    {
        public string Day { get; set; }
        public double Temperature { get; set; }

        public TemperatureRecord(string day, double temp)
        {
            Day = day;
            Temperature = temp;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення колекції температурних записів
            List<TemperatureRecord> records = new()
            {
                new("Понеділок", 22.5),
                new("Вівторок", 27.3),
                new("Середа", 30.1),
                new("Четвер", 24.0),
                new("П’ятниця", 29.8),
                new("Субота", 33.2),
                new("Неділя", 19.5)
            };

            // --- 1. Func<double, bool> (умова відбору)
            Func<double, bool> isHot = t => t > 25.0;

            // --- 2. Action<double> (вивід результату)
            Action<double> printHot = t => Console.WriteLine($"Спекотно: {t}°C");

            // --- 3. LINQ-вираз для вибору днів з температурою >25°C
            var hotDays = records.Where(r => isHot(r.Temperature));

            Console.WriteLine("Дні, коли було спекотно (>25°C):\n");

            foreach (var r in hotDays)
            {
                Console.Write($"{r.Day,-10} → ");
                printHot(r.Temperature);
            }

            // --- 4. Додатково: підрахунок середньої температури спекотних днів
            var avgHot = hotDays.Average(r => r.Temperature);
            Console.WriteLine($"\nСередня температура спекотних днів: {avgHot:F1}°C");
        }
    }
}
