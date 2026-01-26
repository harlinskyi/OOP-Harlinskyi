using System;

namespace lab21v7;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var service = new CloudStorageService();

        Console.WriteLine("=== Система розрахунку Хмарного сховища ===");
        Console.WriteLine("Оберіть тарифний план:");

        // Використовуємо константи для відображення актуальних цін у дужках
        Console.WriteLine($"1. Personal Plan ({StorageConstants.PersonalGbRate} грн/ГБ, {StorageConstants.PersonalUserRate} грн/користувач)");
        Console.WriteLine($"2. Business Plan ({StorageConstants.BusinessGbRate} грн/ГБ, {StorageConstants.BusinessUserRate} грн/користувач)");
        Console.WriteLine($"3. Enterprise Plan (База {StorageConstants.EnterpriseBaseRate} грн, {StorageConstants.EnterpriseGbRate} грн/ГБ, {StorageConstants.EnterpriseUserRate} грн/користувач)");
        Console.WriteLine($"4. Premium Plan (База {StorageConstants.PremiumBaseRate} грн, {StorageConstants.PremiumGbRate} грн/ГБ)");

        Console.Write("\nВаш вибір (1-4): ");
        string choice = Console.ReadLine() ?? string.Empty;

        // ... інша частина коду (введення ГБ та користувачів) ...

        Console.Write("Введіть обсяг даних (ГБ): ");
        double gb = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Введіть кількість користувачів: ");
        int users = int.Parse(Console.ReadLine() ?? "0");

        try
        {
            IStorageStrategy strategy = StoragePlanFactory.CreatePlan(choice);
            decimal cost = service.GetTotalCost(gb, users, strategy);

            Console.WriteLine("\n" + new string('-', 40));
            Console.WriteLine($"Загальна вартість: {cost:F2} грн.");
            Console.WriteLine(new string('-', 40));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}