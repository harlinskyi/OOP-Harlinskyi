namespace lab2
{
    public sealed class BankAccount
    {
        // Баланс
        private decimal balance;

        // Історія транзакцій
        private decimal[] _transactions;

        // Кількість транзакцій
        private int _count;

        public BankAccount(string accountNumber, string owner, decimal initialBalance = 0m, int initialCapacity = 8)
        {
            if (accountNumber is null) throw new ArgumentNullException(nameof(accountNumber));
            if (owner is null) throw new ArgumentNullException(nameof(owner));
            if (initialBalance < 0) throw new ArgumentOutOfRangeException(nameof(initialBalance), "Початковий баланс не може бути від’ємним.");
            if (initialCapacity <= 0) initialCapacity = 8;

            AccountNumber = accountNumber;
            Owner = owner;
            _transactions = new decimal[initialCapacity];

            Balance = initialBalance;
            if (initialBalance != 0) AppendTx(initialBalance);
        }
        // Номер рахунку
        public string AccountNumber { get; }
        public string Owner { get; set; }

        // Баланс
        public decimal Balance
        {
            get => balance;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Balance), "Баланс не може бути від’ємним.");
                balance = value;
            }
        }

        // Поповнення
        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Сума поповнення має бути > 0.");
            Balance = Balance + amount;
            AppendTx(amount);
        }

        // Зняття
        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Сума зняття має бути > 0.");
            if (Balance - amount < 0)
                throw new InvalidOperationException("Недостатньо коштів для зняття.");
            Balance = Balance - amount;
            AppendTx(-amount);
        }

        public int TransactionCount => _count;

        public decimal this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_count)
                    throw new IndexOutOfRangeException($"Індекс {index} поза діапазоном 0..{_count - 1}.");
                return _transactions[index];
            }
        }

        public static BankAccount operator +(BankAccount acc, decimal amount)
        {
            if (acc is null) throw new ArgumentNullException(nameof(acc));
            acc.Deposit(amount);
            return acc;
        }

        public static BankAccount operator -(BankAccount acc, decimal amount)
        {
            if (acc is null) throw new ArgumentNullException(nameof(acc));
            acc.Withdraw(amount);
            return acc;
        }

        public override string ToString() => $"{Owner} | {AccountNumber} | Баланс: {Balance:F2} грн";

        // Helpers
        private void AppendTx(decimal value)
        {
            EnsureCapacity(_count + 1);
            _transactions[_count++] = value;
        }

        private void EnsureCapacity(int required)
        {
            if (required <= _transactions.Length) return;
            int newLen = _transactions.Length << 1;
            if (newLen < required) newLen = required;
            Array.Resize(ref _transactions, newLen);
        }
    }
}
