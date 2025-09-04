namespace lab1v20
{
    public abstract class Figure
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        protected Figure(string name)
        {
            _name = name;
            Console.WriteLine($"Figure {name} created.");
        }

        ~Figure()
        {
            Console.WriteLine($"Figure {_name} destroyed.");
        }

        public virtual double GetArea()
        {
            return 0;
        }
    }

    public class Circle : Figure
    {
        private double _radius;

        public Circle(string name, double radius) : base(name)
        {
            _radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Circle circle = new Circle("MyCircle", rnd.Next(1, 10));
            double area = circle.GetArea();

            Console.WriteLine($"Name: {circle.Name}");
            Console.WriteLine($"Area: {area}");
        }
    }
}