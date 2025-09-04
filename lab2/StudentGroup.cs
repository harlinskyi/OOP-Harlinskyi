namespace lab2v7
{
    public sealed class StudentGroup
    {
        private Student[] _students;
        private int _count;

        public string Name { get; set; }
        public int Count => _count;

        public StudentGroup(string name, int capacity = 8)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            Name = string.IsNullOrWhiteSpace(name) ? "Група" : name.Trim();
            _students = new Student[capacity];
            _count = 0;
        }

        // Індексатор за індексом
        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                return _students[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                _students[index] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        // Індексатор за ПІБ
        public Student? this[string pib]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(pib)) return null;
                var term = pib.Trim();
                for (int i = 0; i < _count; i++)
                {
                    if (string.Equals(_students[i].FullName, term, StringComparison.OrdinalIgnoreCase))
                        return _students[i];
                }
                return null;
            }
        }

        private void EnsureCapacity(int min)
        {
            if (_students.Length >= min) return;
            int newCap = Math.Max(min, _students.Length * 2);
            Array.Resize(ref _students, newCap);
        }

        private int IndexOf(Student s)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_students[i] == s) return i;
                if (_students[i].Equals(s)) return i;
            }
            return -1;
        }

        public bool Contains(Student s) => IndexOf(s) >= 0;

        public bool Add(Student s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));
            if (Contains(s)) return false;
            EnsureCapacity(_count + 1);
            _students[_count++] = s;
            return true;
        }

        public bool Remove(Student s)
        {
            int idx = IndexOf(s);
            if (idx < 0) return false;
            for (int i = idx + 1; i < _count; i++)
                _students[i - 1] = _students[i];
            _students[--_count] = null!;
            return true;
        }

        // Оператори + та -
        public static StudentGroup operator +(StudentGroup g, Student s)
        {
            if (g is null) throw new ArgumentNullException(nameof(g));
            g.Add(s);
            return g;
        }

        public static StudentGroup operator -(StudentGroup g, Student s)
        {
            if (g is null) throw new ArgumentNullException(nameof(g));
            g.Remove(s);
            return g;
        }

        public override string ToString() => $"{Name}: {Count} студент(ів)";
    }
}
