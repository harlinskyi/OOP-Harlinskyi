public abstract class Figure
{
    public string Name { get; set; }

    protected Figure(string name)
    {
        Name = name;
    }

    public virtual double GetArea()
    {
        return 0;
    }

    public virtual double GetVolume()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"{Name}: площа = {GetArea():F2}, об’єм = {GetVolume():F2}";
    }
}

public class Square : Figure

{
    public double Side { get; set; }

    public Square(double side) : base("Квадрат")
    {
        Side = side;
    }

    public override double GetArea()
    {
        return Side * Side;
    }
}

public class Cube : Figure
{
    public double Side { get; set; }

    public Cube(double side) : base("Куб")
    {
        Side = side;
    }

    public override double GetArea()
    {
        return 6 * Side * Side;
    }

    public override double GetVolume()
    {
        return Math.Pow(Side, 3);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Figure> figures = new List<Figure>
        {
            new Square(5),
            new Cube(3),
            new Square(10),
            new Cube(7)
        };

        Console.WriteLine("Результати обчислень:\n");

        foreach (var figure in figures)
        {
            Console.WriteLine(figure);
        }
    }
}