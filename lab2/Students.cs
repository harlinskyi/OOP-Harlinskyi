namespace lab2
{
    public sealed class Student : IEquatable<Student>
    {
        // Приватні поля
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _year;
        private double _average;

        // Властивості
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(Id));
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(FirstName)) : value.Trim();
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(LastName)) : value.Trim();
        }

        public int Year
        {
            get => _year;
            set
            {
                if (value is < 1 or > 6) throw new ArgumentOutOfRangeException(nameof(Year));
                _year = value;
            }
        }

        public double Average
        {
            get => _average;
            set
            {
                if (value is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(Average));
                _average = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public Student(int id, string firstName, string lastName, int year, double average)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
            Average = average;
        }

        public override string ToString() =>
            $"[{Id}] {FullName}, курс: {Year}, середній бал: {Average:F1}";

        // Рівність за Id
        public bool Equals(Student? other) => other is not null && Id == other.Id;
        public override bool Equals(object? obj) => obj is Student s && Equals(s);
        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Student? a, Student? b) =>
            ReferenceEquals(a, b) || (a is not null && b is not null && a.Id == b.Id);

        public static bool operator !=(Student? a, Student? b) => !(a == b);
    }
}
