namespace lab2
{
    public static class Program
    {
        public static void Main()
        {
            var studentsList = new List<Student>([
                new Student(1, "Кирило", "Гарлінський", 3, 84.5),
                new Student(2, "Олексій", "Купець", 3, 91.2),
                new Student(3, "Руслан", "Орешко", 3, 76.0),
                new Student(4, "Іван", "Денисевич", 3, 88.3)
            ]);


            var group = new StudentGroup("КН-3/1", 3);

            studentsList.ForEach(s => group.Add(s));

            Console.WriteLine("Список студентів групи " + group);

            // Show all students
            for (int i = 0; i < group.Count; i++)
                Console.WriteLine($"  #{i}: {group[i]}");

            var found = group["Кирило Гарлінський"];
            Console.WriteLine("Знайдено за ПІБ: " + (found != null ? found.ToString() : "немає"));

            // search student by Name or Surname
            var name = "Кирило Гарлінський";
            found = group[name];
            Console.WriteLine("Пошук за ім'ям або прізвищем для видалення: " + (found != null ? found.ToString() : "не знайдено"));
            if (found != null)
            {
                group.Remove(found);
                Console.WriteLine("Після видалення: " + group);
            }

            Console.WriteLine("Поточний список студентів " + group);

        }
    }
}
