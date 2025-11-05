using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lab6_GenericCache_Extended
{
    // --------------------------
    // 1️⃣ Reference Cache<T>
    // --------------------------
    public class Cache<T> where T : class, new()
    {
        private class CacheItem
        {
            public T Value { get; }
            public DateTime CreatedAt { get; }

            public CacheItem(T value)
            {
                Value = value;
                CreatedAt = DateTime.Now;
            }
        }

        private readonly Dictionary<string, CacheItem> _items = new();
        private readonly TimeSpan _ttl;

        public Cache(TimeSpan ttl)
        {
            _ttl = ttl;
        }

        public void Set(string key, T value)
        {
            _items[key] = new CacheItem(value);
        }

        public T? Get(string key)
        {
            if (_items.TryGetValue(key, out var item))
            {
                if (DateTime.Now - item.CreatedAt < _ttl)
                    return item.Value;
                _items.Remove(key);
            }
            return null;
        }

        public void Cleanup()
        {
            var toRemove = _items
                .Where(kv => DateTime.Now - kv.Value.CreatedAt >= _ttl)
                .Select(kv => kv.Key)
                .ToList();

            foreach (var key in toRemove)
                _items.Remove(key);
        }

        public List<T> GetSorted(Func<T, IComparable> selector, bool ascending = true)
        {
            var values = _items.Values
                               .Where(i => DateTime.Now - i.CreatedAt < _ttl)
                               .Select(i => i.Value)
                               .ToList();

            // Selection sort
            for (int i = 0; i < values.Count - 1; i++)
            {
                int selectedIndex = i;
                for (int j = i + 1; j < values.Count; j++)
                {
                    var a = selector(values[j]);
                    var b = selector(values[selectedIndex]);
                    if (ascending ? a.CompareTo(b) < 0 : a.CompareTo(b) > 0)
                        selectedIndex = j;
                }

                if (selectedIndex != i)
                {
                    var tmp = values[i];
                    values[i] = values[selectedIndex];
                    values[selectedIndex] = tmp;
                }
            }

            return values;
        }

        public int Count => _items.Count;
    }

    // --------------------------
    // 2️⃣ StructCache<T>
    // --------------------------
    public class StructCache<T> where T : struct
    {
        private struct CacheItem
        {
            public T Value;
            public DateTime CreatedAt;

            public CacheItem(T value)
            {
                Value = value;
                CreatedAt = DateTime.Now;
            }
        }

        private readonly List<CacheItem> _items = new();
        private readonly TimeSpan _ttl;

        public StructCache(TimeSpan ttl)
        {
            _ttl = ttl;
        }

        public void Add(T value)
        {
            _items.Add(new CacheItem(value));
        }

        public void Cleanup()
        {
            _items.RemoveAll(i => DateTime.Now - i.CreatedAt >= _ttl);
        }

        public List<T> GetSorted(Func<T, IComparable> selector, bool ascending = true)
        {
            var list = _items
                .Where(i => DateTime.Now - i.CreatedAt < _ttl)
                .Select(i => i.Value)
                .ToList();

            // Selection sort (value-type)
            for (int i = 0; i < list.Count - 1; i++)
            {
                int selectedIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    var a = selector(list[j]);
                    var b = selector(list[selectedIndex]);
                    if (ascending ? a.CompareTo(b) < 0 : a.CompareTo(b) > 0)
                        selectedIndex = j;
                }

                if (selectedIndex != i)
                {
                    var tmp = list[i];
                    list[i] = list[selectedIndex];
                    list[selectedIndex] = tmp;
                }
            }

            return list;
        }

        public int Count => _items.Count;
    }

    // --------------------------
    // 3️⃣ Модельні типи
    // --------------------------
    public class TemperatureRecord
    {
        public string Day { get; set; } = "";
        public double Temperature { get; set; }
        public override string ToString() => $"{Day}: {Temperature}°C";
    }

    public struct Point
    {
        public double X;
        public double Y;
        public double Distance => Math.Sqrt(X * X + Y * Y);
        public override string ToString() => $"({X}, {Y}) | r={Distance:F2}";
    }

    // --------------------------
    // 4️⃣ Демонстрація
    // --------------------------
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Reference Cache<T> (class) ===");
            var refCache = new Cache<TemperatureRecord>(TimeSpan.FromSeconds(3));

            refCache.Set("mon", new TemperatureRecord { Day = "Понеділок", Temperature = 23.5 });
            refCache.Set("tue", new TemperatureRecord { Day = "Вівторок", Temperature = 29.1 });
            refCache.Set("wed", new TemperatureRecord { Day = "Середа", Temperature = 27.3 });

            refCache.GetSorted(r => r.Temperature, ascending: false)
                .ForEach(r => Console.WriteLine($"  {r}"));

            Thread.Sleep(3500);
            refCache.Cleanup();
            Console.WriteLine($"Після очищення: {refCache.Count} елемент(ів)\n");

            Console.WriteLine("=== StructCache<T> (struct) ===");
            var structCache = new StructCache<Point>(TimeSpan.FromSeconds(3));

            structCache.Add(new Point { X = 1, Y = 1 });
            structCache.Add(new Point { X = 2, Y = 5 });
            structCache.Add(new Point { X = 4, Y = 4 });
            structCache.Add(new Point { X = 3, Y = 2 });

            Console.WriteLine("Відсортовано за відстанню від початку координат (зростання):");
            var sortedPoints = structCache.GetSorted(p => p.Distance, ascending: true);
            sortedPoints.ForEach(p => Console.WriteLine($"  {p}"));
        }
    }
}
