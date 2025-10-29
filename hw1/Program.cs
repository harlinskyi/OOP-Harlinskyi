namespace hw1;

/// <summary>
/// Тестовий клас, який задовольняє обмеженням Cache<T>:
/// 1. public class (посилальний тип)
/// 2. має public конструктор без параметрів (new())
/// </summary>
public class TestData
{
    public int Id { get; set; }
    public string Name { get; set; }

    public TestData() { } // Обов'язковий конструктор new()

    public TestData(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"TestData {{ Id: {Id}, Name: '{Name}' }}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("🚀 Демонстрація Generics Cache<T>");

        // Створюємо кеш з максимальним розміром 3
        Cache<TestData> testCache = new Cache<TestData>(maxSize: 3);

        // 1. Демонстрація роботи алгоритму видалення (CleanUpOldest)
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n=== ЕТАП 1: ТЕСТ АЛГОРИТМУ ВИДАЛЕННЯ (FIFO/LRU) ===");
        Console.ResetColor();

        // Додаємо 4 елементи (макс. розмір 3)
        testCache.Add(new TestData(101, "A_First"));
        Thread.Sleep(100); // Забезпечуємо різницю в AddedTime
        testCache.Add(new TestData(102, "B_Second"));
        Thread.Sleep(100);
        testCache.Add(new TestData(103, "C_Third"));
        Thread.Sleep(100);

        // Цей елемент спричинить видалення "A_First"
        testCache.Add(new TestData(104, "D_Fourth"));

        testCache.DisplayCache("Кеш після додавання 4-го елемента (видалено найстаріший)");


        // 2. Демонстрація роботи алгоритму сортування (Selection Sort)
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n=== ЕТАП 2: ТЕСТ АЛГОРИТМУ СОРТУВАННЯ ===");
        Console.ResetColor();

        // Додамо елементи у випадковому порядку, щоб показати сортування
        Thread.Sleep(50);
        testCache.Add(new TestData(105, "E_Newest"));
        testCache.DisplayCache("Кеш перед сортуванням (несортований)");

        // Викликаємо власний алгоритм сортування
        testCache.SortCacheItemsByTime();

        testCache.DisplayCache("Кеш після Selection Sort (від найстарішого до найновішого)");
    }
}