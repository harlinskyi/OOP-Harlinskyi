using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Threading;

namespace IndependentWork11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== STARTING INDEPENDENT WORK 11 (POLLY SCENARIOS) ===\n");

            // Сценарій 1: Підключення до бази даних (Retry)
            RunDatabaseRetryScenario();

            // Сценарій 2: Нестабільний зовнішній сервіс (Circuit Breaker)
            RunCircuitBreakerScenario();

            // Сценарій 3: Довга операція обробки даних (Timeout + Fallback)
            RunTimeoutFallbackScenario();

            Console.WriteLine("\n=== ALL SCENARIOS COMPLETED ===");
        }

        // --------------------------------------------------------------------------------------
        // СЦЕНАРІЙ 1: Доступ до бази даних (Retry)
        // --------------------------------------------------------------------------------------
        static void RunDatabaseRetryScenario()
        {
            Console.WriteLine("--- Scenario 1: Database Connection (Retry Policy) ---");

            int attempts = 0;

            // Імітація методу підключення
            void ConnectToDatabase()
            {
                attempts++;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Attempt #{attempts}: Connecting to SQL Database...");

                // Імітуємо збій перші 2 рази (Transient Failure)
                if (attempts <= 2)
                {
                    throw new Exception("Connection timeout (Transient Error)");
                }

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] SUCCESS: Connected to Database.");
            }

            // Налаштування політики: WaitAndRetry
            // Ми чекаємо, тому що базі даних може знадобитися час на перезавантаження або відновлення мережі.
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(1 * attempt), // 1s, 2s, 3s
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"(!) Exception: {exception.Message}. Retrying in {timeSpan.TotalSeconds}s (Attempt {retryCount})...");
                    }
                );

            try
            {
                retryPolicy.Execute(() => ConnectToDatabase());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FATAL] Database connection failed: {ex.Message}");
            }
            Console.WriteLine();
        }

        // --------------------------------------------------------------------------------------
        // СЦЕНАРІЙ 2: Нестабільний зовнішній API (Circuit Breaker)
        // --------------------------------------------------------------------------------------
        static void RunCircuitBreakerScenario()
        {
            Console.WriteLine("--- Scenario 2: Unstable API (Circuit Breaker) ---");

            // Імітація виклику API
            void CallUnstableApi(int id)
            {
                Console.Write($"[{DateTime.Now:HH:mm:ss}] Request ID {id}: ");
                // Імітуємо, що сервіс "впав" і повертає помилки
                throw new Exception("500 Internal Server Error");
            }

            // Налаштування політики: Circuit Breaker
            // Якщо станеться 2 помилки підряд, "рубильник" вимикається на 5 секунд.
            // Це захищає зовнішній сервіс від перевантаження нашими запитами.
            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(5),
                    onBreak: (ex, breakDelay) =>
                    {
                        Console.WriteLine($"\n[CIRCUIT OPEN] Circuit broken due to: {ex.Message}. Blocked for {breakDelay.TotalSeconds}s.");
                    },
                    onReset: () => Console.WriteLine("\n[CIRCUIT CLOSED] Service recovered. Requests allowed again."),
                    onHalfOpen: () => Console.WriteLine("\n[CIRCUIT HALF-OPEN] Testing service connectivity...")
                );

            // Робимо цикл запитів, щоб побачити зміну станів
            for (int i = 1; i <= 10; i++)
            {
                try
                {
                    // Якщо Circuit Open - цей код навіть не спробує викликати CallUnstableApi
                    circuitBreakerPolicy.Execute(() => CallUnstableApi(i));
                }
                catch (BrokenCircuitException)
                {
                    Console.WriteLine("Request rejected immediately (Circuit is Open).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API Call Failed: {ex.Message}");
                }

                Thread.Sleep(1000); // Пауза між запитами 1 секунда
            }
            Console.WriteLine();
        }

        // --------------------------------------------------------------------------------------
        // СЦЕНАРІЙ 3: Довга операція (Timeout + Fallback)
        // --------------------------------------------------------------------------------------
        static void RunTimeoutFallbackScenario()
        {
            Console.WriteLine("--- Scenario 3: Heavy Calculation (Timeout + Fallback) ---");

            // Імітація повільного методу
            string GetReportData(CancellationToken ct)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Starting report generation (expected 4s)...");
                // Імітуємо роботу, яка триває 4 секунди
                Thread.Sleep(4000);

                // Перевірка на скасування (важливо для Timeout)
                if (ct.IsCancellationRequested) return "Cancelled";

                return "Real Report Data";
            }

            // Політика Timeout: Якщо операція триває довше 2 секунд - перервати.
            // TimeoutStrategy.Pessimistic дозволяє переривати Thread.Sleep (синхронний код)
            var timeoutPolicy = Policy.Timeout(TimeSpan.FromSeconds(2), TimeoutStrategy.Pessimistic);

            // Політика Fallback: Якщо трапився Timeout (або інша помилка), повернути заглушку.
            var fallbackPolicy = Policy<string>
                .Handle<TimeoutRejectedException>()
                .Fallback(
                    fallbackValue: "Cached/Default Report",
                    onFallback: (ex) => Console.WriteLine($"(!) Operation timed out. Serving fallback value.")
                );

            // ВИПРАВЛЕННЯ ТУТ:
            // Використовуємо метод екземпляра .Wrap(), щоб явно вказати:
            // "Fallback (який повертає string) обгортає Timeout (який нічого не повертає, а просто контролює час)"
            var policyWrap = fallbackPolicy.Wrap(timeoutPolicy);

            try
            {
                // Execute повинен приймати CancellationToken для правильної роботи Timeout
                string result = policyWrap.Execute((ct) => GetReportData(ct), CancellationToken.None);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] RESULT: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}