using lab2;

internal class Program
{
    private static void Main()
    {
        try
        {
            var acc = new BankAccount("UA1234567890", "Harlinskyi Kyrylo", initialBalance: 500m, initialCapacity: 4);
            Console.WriteLine("Після створення:   " + acc);

            Console.WriteLine("Поточний баланс (get): " + acc.Balance);

            acc.Deposit(200m);
            Console.WriteLine("Після Deposit(200):    " + acc);

            acc.Withdraw(50m);
            Console.WriteLine("Після Withdraw(50):    " + acc);

            acc.Deposit(300m);
            Console.WriteLine("Після оператора '+300': " + acc);

            acc.Withdraw(100m);
            Console.WriteLine("Після оператора '-100': " + acc);

            Console.WriteLine("\nІсторія транзакцій:");
            for (int i = 0; i < acc.TransactionCount; i++)
            {
                var op = acc[i];
                var kind = op >= 0 ? "Депозит" : "Зняття";
                Console.WriteLine($"[{i}] {kind}: {op:F2} грн");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
