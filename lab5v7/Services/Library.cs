using Lab5v7.Models;
using Lab5v7.Repository;

namespace Lab5v7.Services;

public class Library
{
    private readonly Repository<Loan> _loanRepo = new();

    public void AddLoan(Loan loan) => _loanRepo.Add(loan);
    public IEnumerable<Loan> AllLoans() => _loanRepo.All();

    public IEnumerable<Loan> GetLoansByReader(string readerName) =>
        _loanRepo.Where(l => l.Reader.Name.Equals(readerName, StringComparison.OrdinalIgnoreCase));

    public decimal TotalFines() => _loanRepo.All().Sum(l => l.Fine());
}