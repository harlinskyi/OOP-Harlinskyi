using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Lab7
{
    // ==========================================
    // 1. Узагальнений клас RetryHelper
    // ==========================================
    public static class RetryHelper
    {
        /// <summary>
        /// Виконує операцію з механізмом повторних спроб (Retry).
        /// </summary>
        /// <typeparam name="T">Тип результату операції.</typeparam>
        /// <param name="operation">Делегат операції, яку потрібно виконати.</param>
        /// <param name="retryCount">Кількість повторних спроб (за замовчуванням 3).</param>
        /// <param name="initialDelay">Початкова затримка. Якщо null, береться 1 секунда.</param>
        /// <param name="shouldRetry">Функція для перевірки типу винятку. Якщо null, повторюємо для всіх.</param>
        /// <returns>Результат виконання операції.</returns>
        public static T ExecuteWithRetry<T>(
            Func<T> operation,
            int retryCount = 3,
            TimeSpan initialDelay = default,
            Func<Exception, bool> shouldRetry = null)
        {
            // Якщо затримка не передана, встановлюємо дефолтну (наприклад, 1 сек)
            if (initialDelay == default)
            {
                initialDelay = TimeSpan.FromSeconds(1);
            }

            int attempt = 0;

            while (true)
            {
                try
                {
                    // Спроба виконати операцію
                    return operation();
                }
                catch (Exception ex)
                {
                    // 1. Перевіряємо, чи це остання спроба
                    if (attempt >= retryCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[Error] Вичерпано ліміт спроб. Остання помилка: {ex.Message}");
                        Console.ResetColor();
                        throw; // Прокидаємо виняток далі
                    }

                    // 2. Перевіряємо, чи підходить цей тип винятку для повтору (через shouldRetry)
                    if (shouldRetry != null && !shouldRetry(ex))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[Error] Виняток {ex.GetType().Name} не підлягає повторній обробці.");
                        Console.ResetColor();
                        throw;
                    }

                    // 3. Розрахунок експоненційної затримки (Delay * 2^attempt)
                    // Math.Pow(2, attempt) дасть 1, 2, 4...
                    double multiplier = Math.Pow(2, attempt);
                    TimeSpan delay = TimeSpan.FromTicks((long)(initialDelay.Ticks * multiplier));

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[Retry] Спроба #{attempt + 1} провалилась ({ex.Message}). " +
                                      $"Очікування {delay.TotalSeconds} сек перед наступною спробою...");
                    Console.ResetColor();

                    // Блокуємо потік на час затримки
                    Thread.Sleep(delay);

                    attempt++;
                }
            }
        }
    }

    // ==========================================
    // 2. Клас FileProcessor (Імітація)
    // ==========================================
    public class FileProcessor
    {
        private int _attemptsCount = 0;

        // За завданням: Імітувати IOException перші 3 рази, потім успіх.
        public List<string> LoadUsernames(string path)
        {
            _attemptsCount++;
            Console.WriteLine($"-> FileProcessor: Спроба читання файлу '{path}' (Спроба #{_attemptsCount})");

            if (_attemptsCount <= 3)
            {
                throw new IOException("Файл заблоковано іншим процесом або помилка читання.");
            }

            // Успішний результат
            return new List<string> { "admin", "guest", "root", "user1" };
        }
    }

    // ==========================================
    // 3. Клас NetworkClient (Імітація)
    // ==========================================
    public class NetworkClient
    {
        private int _attemptsCount = 0;

        // За завданням: Імітувати HttpRequestException перші 2 рази, потім успіх.
        public List<string> GetUsersFromApi(string url)
        {
            _attemptsCount++;
            Console.WriteLine($"-> NetworkClient: Спроба HTTP GET запиту на '{url}' (Спроба #{_attemptsCount})");

            if (_attemptsCount <= 2)
            {
                // У нових версіях .NET HttpRequestException може не приймати лише повідомлення, 
                // але для імітації цього достатньо.
                throw new HttpRequestException("Помилка з'єднання з сервером (503 Service Unavailable).");
            }

            // Успішний результат
            return new List<string> { "api_user_1", "api_user_2", "api_bot" };
        }
    }

    // ==========================================
    // 4. Main Program
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Щоб коректно відображалась кирилиця
            Console.WriteLine("=== Лабораторна робота №7: Патерн Retry ===\n");

            // Ініціалізація сервісів
            var fileProcessor = new FileProcessor();
            var networkClient = new NetworkClient();

            // Визначаємо правило shouldRetry: повторювати тільки для IO та Http помилок
            Func<Exception, bool> retryLogic = (ex) =>
                ex is IOException || ex is HttpRequestException;

            // --- СЦЕНАРІЙ 1: Робота з файлами ---
            Console.WriteLine("--- Тест 1: Завантаження користувачів з файлу ---");
            try
            {
                // Завдання: RetryCount = 3 (або більше, щоб покрити 3 невдачі), початкова затримка 1 сек
                List<string> fileUsers = RetryHelper.ExecuteWithRetry(
                    operation: () => fileProcessor.LoadUsernames("users.txt"),
                    retryCount: 4,
                    initialDelay: TimeSpan.FromSeconds(1),
                    shouldRetry: retryLogic
                );

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Success] Користувачі з файлу завантажені: {string.Join(", ", fileUsers)}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Fatal] Операція провалилась остаточно: {ex.Message}");
            }

            Console.WriteLine(new string('-', 40) + "\n");

            // --- СЦЕНАРІЙ 2: Робота з мережею ---
            Console.WriteLine("--- Тест 2: Отримання користувачів через API ---");
            try
            {
                // Завдання: RetryCount = 3, початкова затримка 0.5 сек
                List<string> apiUsers = RetryHelper.ExecuteWithRetry(
                    operation: () => networkClient.GetUsersFromApi("https://api.example.com/users"),
                    retryCount: 3,
                    initialDelay: TimeSpan.FromSeconds(0.5),
                    shouldRetry: retryLogic
                );

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[Success] Користувачі з API отримані: {string.Join(", ", apiUsers)}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Fatal] Операція провалилась остаточно: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}