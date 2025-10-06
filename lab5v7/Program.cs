using Lab5v7.Models;
using Lab5v7.Services;
using Lab5v7.Exceptions;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// --- Ініціалізація бібліотеки ---
var library = new Library();

// --- Створення тестових даних ---
var reader1 = new Reader { Id = 1, Name = "Кирило Гарлінський" };
var reader2 = new Reader { Id = 2, Name = "Олексій Купець" };

var book1 = new Book { Id = 1, Title = "C# для початківців", Author = "М. Сміт" };
var book2 = new Book { Id = 2, Title = "LINQ у прикладах", Author = "А. Джонсон" };

// --- Позики ---
var loan1 = new Loan { Reader = reader1, Book = book1, IssueDate = DateTime.Now.AddDays(-20) };
var loan2 = new Loan { Reader = reader2, Book = book2, IssueDate = DateTime.Now.AddDays(-10) };

try
{
    // Виклик винятку: повернення раніше дати видачі
    loan1.ReturnBook(DateTime.Now.AddDays(-25));
}
catch (InvalidReturnDateException ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
    // Коректне повернення
    loan1.ReturnBook(DateTime.Now);
}

// Додаємо позики до бібліотеки
library.AddLoan(loan1);
library.AddLoan(loan2);

// --- Обчислення з LINQ ---
var overdueLoans = library.AllLoans().Where(l => l.DaysOverdue() > 0).ToList();
var totalFine = overdueLoans.Sum(l => l.Fine());
var avgFine = overdueLoans.Any() ? overdueLoans.Average(l => l.Fine()) : 0;

// --- Вивід результатів ---
Console.WriteLine("\n===== ПОЗИКИ В БІБЛІОТЕЦІ =====");
Console.WriteLine("{0,-20} | {1,-25} | {2,10} | {3,10}",
    "Читач", "Книга", "Днів простр.", "Штраф, ₴");
Console.WriteLine(new string('-', 75));

foreach (var loan in library.AllLoans())
{
    Console.WriteLine("{0,-20} | {1,-25} | {2,10} | {3,10:F2}",
        loan.Reader.Name,
        loan.Book.Title,
        loan.DaysOverdue(),
        loan.Fine());
}

Console.WriteLine(new string('-', 75));
Console.WriteLine($"Загальний штраф: {totalFine:F2} ₴");
Console.WriteLine($"Середній штраф: {avgFine:F2} ₴");
