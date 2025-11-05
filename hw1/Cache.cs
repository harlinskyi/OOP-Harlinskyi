namespace hw1;

/// <summary>
/// Узагальнений кеш з обмеженням: T має бути посилальним типом і мати конструктор без параметрів.
/// </summary>
/// <typeparam name="T">Тип даних, що зберігаються у кеші.</typeparam>
public class Cache<T> where T : class, new()
{
    /// <summary>
    /// Внутрішній клас для зберігання значення та часу його додавання.
    /// </summary>
    private class CacheItem
    {
        public T Value { get; set; }
        public DateTime AddedTime { get; } = DateTime.Now;

        public override string ToString()
        {
            return $"[{AddedTime:HH:mm:ss.fff}] {Value}";
        }
    }

    private List<CacheItem> _cacheItems = new List<CacheItem>();
    private readonly int _maxSize;

    /// <summary>
    /// Конструктор кешу.
    /// </summary>
    /// <param name="maxSize">Максимальна кількість елементів у кеші.</param>
    public Cache(int maxSize)
    {
        _maxSize = maxSize > 0 ? maxSize : 10;
    }

    // --- Основні операції ---

    public void Add(T item)
    {
        Console.WriteLine($"\n[ADD] Додано елемент: {item}");
        _cacheItems.Add(new CacheItem { Value = item });
        CleanUpOldest(); // Викликаємо очищення при додаванні
    }

    public List<T> GetAll()
    {
        return _cacheItems.Select(i => i.Value).ToList();
    }

    // --- 🧹 Алгоритм Видалення Старих Елементів ---

    /// <summary>
    /// Видаляє найстаріші елементи (з найменшим AddedTime), якщо розмір перевищує _maxSize.
    /// </summary>
    public void CleanUpOldest()
    {
        if (_cacheItems.Count <= _maxSize) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[CLEANUP] Розмір ({_cacheItems.Count}) > Макс. Розмір ({_maxSize}). Виконуємо видалення...");
        Console.ResetColor();

        while (_cacheItems.Count > _maxSize)
        {
            if (_cacheItems.Count == 0) return;

            // 1. Знаходимо індекс найстарішого елемента (з мінімальним AddedTime)
            int oldestIndex = 0;
            for (int i = 1; i < _cacheItems.Count; i++)
            {
                if (_cacheItems[i].AddedTime < _cacheItems[oldestIndex].AddedTime)
                {
                    oldestIndex = i;
                }
            }

            // 2. Видаляємо найстаріший елемент
            Console.WriteLine($"\tВидалено найстаріший: {_cacheItems[oldestIndex]}");
            _cacheItems.RemoveAt(oldestIndex);
        }
    }

    // --- ⚙️ Алгоритм Сортування (Selection Sort) ---

    /// <summary>
    /// Сортує внутрішній список _cacheItems за AddedTime у порядку зростання
    /// (від найстарішого до найновішого) за допомогою Selection Sort.
    /// </summary>
    public void SortCacheItemsByTime()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n[SORT] Виконуємо Selection Sort за AddedTime...");
        Console.ResetColor();

        int n = _cacheItems.Count;
        for (int i = 0; i < n - 1; i++)
        {
            // Знаходимо індекс найстарішого елемента в невідсортованій частині
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (_cacheItems[j].AddedTime < _cacheItems[minIndex].AddedTime)
                {
                    minIndex = j;
                }
            }

            // Обмінюємо елементи
            if (minIndex != i)
            {
                CacheItem temp = _cacheItems[i];
                _cacheItems[i] = _cacheItems[minIndex];
                _cacheItems[minIndex] = temp;
            }
        }
    }

    /// <summary>
    /// Виводить вміст кешу, включаючи час додавання.
    /// </summary>
    public void DisplayCache(string header)
    {
        Console.WriteLine($"\n--- {header} (Кількість: {_cacheItems.Count}) ---");
        foreach (var item in _cacheItems)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("----------------------------------");
    }
}