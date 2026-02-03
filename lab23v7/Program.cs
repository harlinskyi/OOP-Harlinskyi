using lab23v7.Legacy;
using lab23v7.Refactored;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        // 1. Як було (Legacy)
        Console.WriteLine("--- До рефакторингу (Порушення DIP/ISP) ---");
        var oldComputer = new lab23v7.Legacy.CarComputer();
        oldComputer.StartCar();

        Console.WriteLine("\n" + new string('-', 40) + "\n");

        // 2. Як стало (Refactored)
        Console.WriteLine("--- Після рефакторингу (DIP + ISP + DI) ---");

        IEngine engine = new ModernEngine();
        IRadio radio = new SonyRadio();
        IGPS gps = new GarminGPS();

        var newComputer = new lab23v7.Refactored.CarComputer(engine, radio, gps);
        newComputer.RunSystem();

        Console.ReadLine();
    }
}