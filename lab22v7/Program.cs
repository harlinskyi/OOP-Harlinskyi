using System;

namespace lab22
{
    // ==========================================================
    // ПОРУШЕННЯ LSP
    // ==========================================================
    
    public class User
    {
        public virtual void ChangePassword(string newPassword)
        {
            Console.WriteLine($"Пароль змінено на: {newPassword}");
        }
    }

    public class GuestUser : User
    {
        public override void ChangePassword(string newPassword)
        {
            // Порушення LSP: Гість не може змінювати пароль.
            // Викидання винятку там, де клієнт очікує успішну дію.
            throw new UnauthorizedAccessException("Гість не має права змінювати пароль!");
        }
    }

    // ==========================================================
    // РЕФАКТОРИНГ: ДОТРИМАННЯ LSP (Зміна ієрархії через інтерфейси)
    // ==========================================================

    public abstract class BaseAccount
    {
        public required string Username { get; set; }
        public abstract void DisplayInfo();
    }

    public interface IPasswordChangeable
    {
        void ChangePassword(string newPassword);
    }

    public class RegisteredUser : BaseAccount, IPasswordChangeable
    {
        public override void DisplayInfo() => Console.WriteLine($"Користувач: {Username}");

        public void ChangePassword(string newPassword)
        {
            Console.WriteLine($"[LSP OK] Пароль для {Username} змінено на: {newPassword}");
        }
    }

    public class GuestAccount : BaseAccount
    {
        public override void DisplayInfo() => Console.WriteLine("Гість (лише перегляд)");
        // Метод ChangePassword тут відсутній, що логічно для гостя.
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- Демонстрація порушення ---
            Console.WriteLine("=== Проблема (Порушення LSP) ===");
            User guestAsUser = new GuestUser();
            try
            {
                ProcessUser(guestAsUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine("\n=== Виправлене рішення (LSP OK) ===");
            // --- Демонстрація коректної роботи ---
            var regUser = new RegisteredUser { Username = "Admin_Engineer" };
            var guest = new GuestAccount { Username = "Гість" };

            PrintAccountInfo(regUser);
            PrintAccountInfo(guest);

            // Клієнт тепер працює тільки з тими, хто ПІДТРИМУЄ зміну пароля
            UpdatePasswordSafe(regUser, "CyberPunk_2026");
            
            // guest не пройде в UpdatePasswordSafe на етапі компіляції!
        }

        // Клієнтський метод, що демонструє проблему
        static void ProcessUser(User user)
        {
            // Клієнт очікує, що будь-який User може змінити пароль
            user.ChangePassword("12345"); 
        }

        static void PrintAccountInfo(BaseAccount account)
        {
            account.DisplayInfo();
        }

        static void UpdatePasswordSafe(IPasswordChangeable account, string pass)
        {
            account.ChangePassword(pass);
        }
    }
}