using Lab5v7.Exceptions;

namespace Lab5v7.Models;

public class Loan
{
    public required Reader Reader { get; set; }
    public required Book Book { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public void ReturnBook(DateTime returnDate)
    {
        if (returnDate < IssueDate)
            throw new InvalidReturnDateException("Дата повернення не може бути раніше дати видачі.");
        ReturnDate = returnDate;
    }

    public int DaysOverdue(int allowedDays = 14)
    {
        var dueDate = IssueDate.AddDays(allowedDays);
        var actualDate = ReturnDate ?? DateTime.Now;
        return Math.Max(0, (actualDate - dueDate).Days);
    }

    public decimal Fine(decimal ratePerDay = 5m) => DaysOverdue() * ratePerDay;
}
